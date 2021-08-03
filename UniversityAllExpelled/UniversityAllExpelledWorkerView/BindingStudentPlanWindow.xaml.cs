using System;
using System.Collections.Generic;
using System.Linq;
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
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;

namespace UniversityAllExpelledWorkerView
{
    /// <summary>
    /// Логика взаимодействия для BindingStudentPlanWindow.xaml
    /// </summary>
    public partial class BindingStudentPlanWindow : Window
    {
        private readonly EducationPlanLogic _epLogic;
        private readonly StudentLogic _studentLogic;

        public string Login { set { login = value; } }

        private string login;
        public BindingStudentPlanWindow(EducationPlanLogic epLogic, StudentLogic studentLogic)
        {
            InitializeComponent();
            _epLogic = epLogic;
            _studentLogic = studentLogic;
        }

        private void LoadData()
        {
            try
            {
                ComboBoxStudent.ItemsSource = _studentLogic.Read(new StudentBindingModel
                {
                    //GradebookNumber = login
                });
                ListBoxPlan.ItemsSource = _epLogic.Read(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BindingStudentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
