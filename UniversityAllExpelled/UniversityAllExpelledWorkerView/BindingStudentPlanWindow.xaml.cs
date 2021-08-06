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
using UniversityBusinessLogic.ViewModels;

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
                ListBoxPlan.ItemsSource = _epLogic.Read(null);
                ComboBoxStudent.ItemsSource = _studentLogic.Read(new StudentBindingModel
                {
                    DenearyLogin = login
                });
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

        private void ButtonBinding_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxStudent.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ListBoxPlan.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите план", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var student = _studentLogic.Read(new StudentBindingModel { GradebookNumber = (ComboBoxStudent.SelectedItem as StudentViewModel).GradebookNumber })?[0];
                var ep = _epLogic.Read(new EducationPlanBindingModel { Id = (ListBoxPlan.SelectedItem as EducationPlanViewModel).Id })?[0];
                if (student == null)
                {
                    throw new Exception("Такой студент не найден");
                }
                if (ep == null)
                {
                    throw new Exception("Такой план не найден");
                }
                if (student.EducationPlans.ContainsKey(ep.Id))
                {
                    throw new Exception("Студент уже привязан к данному плану");
                }
                _studentLogic.BindingPlan(student.GradebookNumber, ep.Id);
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
