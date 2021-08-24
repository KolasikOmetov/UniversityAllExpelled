
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_ToMail_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
