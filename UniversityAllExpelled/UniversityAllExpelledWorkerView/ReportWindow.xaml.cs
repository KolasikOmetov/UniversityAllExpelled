using Microsoft.Reporting.WebForms.Internal.Soap.ReportingServices2005.Execution;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWorkerView
{
    /// <summary>
    /// Логика взаимодействия для Report.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public string Login { set { login = value; } }

        private string login;

        private readonly ReportWorkerLogic _logic;
        private readonly StudentLogic _logicStudent;
        private readonly DenearyLogic _logicDeneary;

        public ReportWindow(ReportWorkerLogic logic, StudentLogic studentLogic, DenearyLogic denearyLogic)
        {
            _logic = logic;
            _logicDeneary = denearyLogic;
            _logicStudent= studentLogic;
            InitializeComponent();
        }

        private void ReportWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxStudent.ItemsSource = _logicStudent.Read(new StudentBindingModel { DenearyLogin = login });
        }

        private void Button_Make_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate == null || DatePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Вы не указали дату начала или дату окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxStudent.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не указали дисциплину", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var student = (StudentViewModel)ComboBoxStudent.SelectedItem;
                string desc = $"{student.Name}\nc {DatePickerFrom.SelectedDate.Value.ToShortDateString()} по {DatePickerTo.SelectedDate.Value.ToShortDateString()}";
                ReportParameter parameterPeriod = new ReportParameter("ReportParameterPeriod", desc);
                reportViewer.LocalReport.SetParameters(parameterPeriod);

                var dataSource = _logic.GetCheckLists(new ReportBindingModel
                {
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate,
                    SubjectId = student.Id
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

        private void Button_ToMail_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                var student = (StudentViewModel)ComboBoxStudent.SelectedItem;
                var department = _logicDeneary.Read(new DenearyBindingModel { Login = login })[0];
                msg.Subject = "Отчёт по студенту";
                msg.Body = $"Отчёт по студенту {student.Name} за период c " + DatePickerFrom.SelectedDate.Value.ToShortDateString() +
                " по " + DatePickerTo.SelectedDate.Value.ToShortDateString();
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
