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
    /// Логика взаимодействия для CertificationWindow.xaml
    /// </summary>
    public partial class CertificationWindow : Window
    {
        public int Id { set { id = value; } }

        private int? id;
        public string StudentGradebookNumber
        {
            get { return (ComboBoxStudent.SelectedItem as StudentViewModel).GradebookNumber; }
            set
            {
                studentGbn = value;
            }
        }

        private string studentGbn;

        private readonly CertificationLogic _logicCertification;
        private readonly StudentLogic _logicStudent;

        public CertificationWindow(CertificationLogic certificationLogic, StudentLogic studentLogic)
        {
            InitializeComponent();
            this._logicCertification = certificationLogic;
            this._logicStudent = studentLogic;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Заполните дату проведения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxStudent.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите студента", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicCertification.CreateOrUpdate(new CertificationBindingModel
                {
                    Id = id,
                    Date = (DateTime)DatePicker.SelectedDate,
                    StudentGradebookNumber = studentGbn
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CertificationWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxStudent.ItemsSource = _logicStudent.Read(null);
            if (id.HasValue)
            {
                try
                {
                    ComboBoxStudent.SelectedItem = SetValue(studentGbn);
                    var view = _logicCertification.Read(new CertificationBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        DatePicker.SelectedDate = view.Date;
                        ComboBoxStudent.SelectedItem = view.StudentName;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private StudentViewModel SetValue(string value)
        {
            foreach (var item in ComboBoxStudent.Items)
            {
                if ((item as StudentViewModel).GradebookNumber == value)
                {
                    return item as StudentViewModel;
                }
            }
            return null;
        }
    }
}
