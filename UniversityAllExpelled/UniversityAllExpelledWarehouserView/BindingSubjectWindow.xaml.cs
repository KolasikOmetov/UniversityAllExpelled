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

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для BindingSubjectWindow.xaml
    /// </summary>
    public partial class BindingSubjectWindow : Window
    {
		private readonly SubjectLogic _subjectLogic;
		private readonly StudentLogic _studentLogic;
		public string Login { set { login = value; } }

		private string login;
		public BindingSubjectWindow(SubjectLogic subjectLogic, StudentLogic studentLogic)
        {
            InitializeComponent();
			_subjectLogic = subjectLogic;
			_studentLogic = studentLogic;
		}

        private void BindingSubjectWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

		private void LoadData()
		{
			try
			{
				ComboBoxSubject.ItemsSource = _subjectLogic.Read(new SubjectBindingModel { 
					DepartmentLogin = login
				});
				ListBoxStudent.ItemsSource = _studentLogic.Read(null);
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

        private void ButtonBinding_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxSubject.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите предмет", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ListBoxStudent.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var student = _studentLogic.Read(new StudentBindingModel { GradebookNumber = (ListBoxStudent.SelectedItem as StudentViewModel).GradebookNumber })?[0];
                var subject = _subjectLogic.Read(new SubjectBindingModel { Id = (ComboBoxSubject.SelectedItem as SubjectViewModel).Id })?[0];
                if (student == null)
                {
                    throw new Exception("Такой студент не найден");
                }
                if (subject == null)
                {
                    throw new Exception("Такой предмет не найден");
                }
                if (student.Subjects.ContainsKey(subject.Id))
                {
                    throw new Exception("Студент уже привязан к данному предмету");
                }
                _studentLogic.BindingSubject(student.GradebookNumber, subject.Id);
                MessageBox.Show("Привязка прошла успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
