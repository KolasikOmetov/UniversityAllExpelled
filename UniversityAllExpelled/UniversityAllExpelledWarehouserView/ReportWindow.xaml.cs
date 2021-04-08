using Microsoft.Reporting.WinForms;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using Unity;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        [Dependency]
            public IUnityContainer Container { get; set; }

            private readonly ReportLogic logic;

            public ReportWindow(ReportLogic logic)
            {
                InitializeComponent();
                this.logic = logic;
            }

            private void ReportViewer_Load(object sender, EventArgs e)
            {
                reportViewer.LocalReport.ReportPath = "../../Report.rdlc";
            }

            private void buttonMake_Click(object sender, RoutedEventArgs e)
            {
                if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
                {
                    MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                try
                {
                    ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                    "c " + datePickerFrom.SelectedDate.Value.ToShortDateString() +
                    " по " + datePickerTo.SelectedDate.Value.ToShortDateString());
                    reportViewer.LocalReport.SetParameters(parameter);
                    var dataSource = logic.GetCosmetics(new ReportBindingModel
                    {
                        DateFrom = datePickerFrom.SelectedDate,
                        DateTo = datePickerTo.SelectedDate
                    });
                    ReportDataSource source = new ReportDataSource("DataSetCosmetics", dataSource);
                    reportViewer.LocalReport.DataSources.Add(source);
                    reportViewer.RefreshReport();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            private void buttonToPdf_Click(object sender, RoutedEventArgs e)
            {
                if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
                {
                    MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(TextBoxEmail.Text))
                {
                    MessageBox.Show("Заполните адрес электронной почты", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MailMessage msg = new MailMessage();
                SmtpClient client = new SmtpClient();
                try
                {
                    msg.Subject = "Отчет по косметике";
                    msg.Body = "Отчет по косметике за период c " + datePickerFrom.SelectedDate.Value.ToShortDateString() +
                    " по " + datePickerTo.SelectedDate.Value.ToShortDateString();
                    msg.From = new MailAddress("ksenia.savkina2001@gmail.com");
                    msg.To.Add(TextBoxEmail.Text);
                    msg.IsBodyHtml = true;
                    logic.SaveCosmeticsToPdfFile(new ReportBindingModel
                    {
                        FileName = "D:\\Otchet.pdf",
                        DateFrom = datePickerFrom.SelectedDate,
                        DateTo = datePickerTo.SelectedDate
                    });
                    string file = "D:\\Otchet.pdf";
                    Attachment attach = new Attachment(file, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attach.ContentDisposition;
                    disposition.CreationDate = System.IO.File.GetCreationTime(file);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                    msg.Attachments.Add(attach);
                    client.Host = "smtp.gmail.com";
                    NetworkCredential basicauthenticationinfo = new NetworkCredential("ksenia.savkina2001@gmail.com", "пароль");
                    client.Port = int.Parse("587");
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

            private void TextBoxEmail_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
            {
                TextBoxEmail.Clear();
            }
        }
}
