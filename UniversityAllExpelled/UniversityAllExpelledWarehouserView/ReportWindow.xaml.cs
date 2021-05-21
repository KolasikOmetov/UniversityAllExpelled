using Microsoft.Reporting.WinForms;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public string Login { set { login = value; } }

        private string login;

        private readonly ReportLogic _logic;
        private readonly SubjectLogic _logicSubject;
        private readonly DepartmentLogic _logicDepartment;
        public ReportWindow(ReportLogic logic, SubjectLogic logicSubject, DepartmentLogic departmentLogic)
        {
            _logic = logic;
            _logicSubject = logicSubject;
            _logicDepartment = departmentLogic;
            InitializeComponent();
        }

        private void ReportViewer_Load(object sender, EventArgs e)
        {
            reportViewer.LocalReport.ReportPath = "../../Report.rdlc";
        }

        private void ReportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxSubject.ItemsSource = _logicSubject.Read(new SubjectBindingModel { DepartmentLogin = login });
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerFrom.SelectedDate == null || datePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Вы не указали дату начала или дату окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxSubject.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не указали дисциплину", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var subject = (SubjectViewModel)ComboBoxSubject.SelectedItem;
                string desc = $"{subject.Name}\nc {datePickerFrom.SelectedDate.Value.ToShortDateString()} по {datePickerTo.SelectedDate.Value.ToShortDateString()}";
                ReportParameter parameterPeriod = new ReportParameter("ReportParameterPeriod", desc );
                reportViewer.LocalReport.SetParameters(parameterPeriod);

                var dataSource = _logic.GetCheckLists(new ReportBindingModel
                {
                    DateFrom = datePickerFrom.SelectedDate,
                    DateTo = datePickerTo.SelectedDate,
                    SubjectId = subject.Id
                });
                ReportDataSource source = new ReportDataSource("DataSetSubject", dataSource);
                reportViewer.LocalReport.DataSources.Clear();
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        [Obsolete]
        private void ButtonSendToMail(object sender, RoutedEventArgs e)
        {
            if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                var subject = (SubjectViewModel)ComboBoxSubject.SelectedItem;
                var department = _logicDepartment.Read(new DepartmentBindingModel { DepartmentLogin = login })[0];
                msg.Subject = "Отчёт по дисциплине";
                msg.Body = $"Отчёт по дисциплине {subject.Name} за период c " + datePickerFrom.SelectedDate.Value.ToShortDateString() +
                " по " + datePickerTo.SelectedDate.Value.ToShortDateString();
                msg.From = new MailAddress(App.emailSender);
                msg.To.Add(department.Email);
                msg.IsBodyHtml = true;
                _logic.SaveCheckListsByDateBySubjectToPdfFile(new ReportBindingModel
                {
                    FileName = App.defaultReportPath,
                    DateFrom = datePickerFrom.SelectedDate,
                    DateTo = datePickerTo.SelectedDate,
                    SubjectId = subject.Id
                });
                string file = App.defaultReportPath;
                Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attach.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                msg.Attachments.Add(attach);
                client.Host = App.emailHost;
                client.Port = App.emailPort;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(App.emailSender, App.emailPassword);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
                MessageBox.Show("Сообщение отправлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

