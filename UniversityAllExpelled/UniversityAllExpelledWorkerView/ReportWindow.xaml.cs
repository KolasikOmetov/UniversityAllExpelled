
using Microsoft.Win32;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.HelperModels;
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

        private void Button_Make_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate == null || DatePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Вы не указали дату начала или дату окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var dataSource = _logic.GetEducationPlanStudentsSubjects(new ReportEducationPlanBindingModel
                {
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate
                });
                DataGridReport.ItemsSource = dataSource;
                for (int i = 5; i < 10; i++)
                {
                    DataGridReport.Columns[i].Visibility = Visibility.Hidden;
                }
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
                var deneary = _logicDeneary.Read(new DenearyBindingModel { Login = login })[0];
                var fileName = "Отчет.pdf";
                msg.Subject = "Отчёт по планам";
                msg.Body = $"Отчёт по планам за период c " + DatePickerFrom.SelectedDate.Value.ToShortDateString() +
                " по " + DatePickerTo.SelectedDate.Value.ToShortDateString();
                msg.From = new MailAddress("tmrakhmtv@gmail.com");
                msg.To.Add(deneary.Email);
                msg.IsBodyHtml = true;
                _logic.SaveEPStudentsSubjectsToPdf(new ReportEducationPlanBindingModel
                {
                    FileName = fileName,
                    DateFrom = DatePickerFrom.SelectedDate,
                    DateTo = DatePickerTo.SelectedDate
                });
                Attachment attach = new Attachment(fileName, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = attach.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(fileName);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(fileName);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(fileName);
                msg.Attachments.Add(attach);
                client.Host = "smtp.gmail.com";
                NetworkCredential basicauthenticationinfo = new NetworkCredential("courseworkusing@gmail.com", "321ewq#@!");
                client.Port = 587;
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicauthenticationinfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
                MessageBox.Show("Сообщение отправлено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Button_ToPDF_Click(object sender, RoutedEventArgs e)
        {
            if (DatePickerFrom.SelectedDate >= DatePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" };
            {
                if (dialog.ShowDialog() == true)
                {
                    try
                    {
                        _logic.SaveEPStudentsSubjectsToPdf(new ReportEducationPlanBindingModel
                        {
                            FileName = dialog.FileName,
                            DateFrom = DatePickerFrom.SelectedDate,
                            DateTo = DatePickerTo.SelectedDate,
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                       
                    }
                }
            }
        }
    }
}
