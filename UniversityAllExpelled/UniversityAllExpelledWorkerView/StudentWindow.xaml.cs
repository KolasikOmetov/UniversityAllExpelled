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
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;

namespace UniversityAllExpelledWorkerView
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        public string GrdBookNum { set { gbn = value; } }

        private string gbn;

        public string Login { set { login = value; } }

        private string login;

        private readonly StudentLogic _logicStudent;

        public bool isUpdating;

        public StudentWindow(StudentLogic logic)
        {
            InitializeComponent();
            this._logicStudent = logic;
        }

        private void StudentWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(gbn))
            {
                try
                {
                    var view = _logicStudent.Read(new StudentBindingModel { GradebookNumber = gbn })?[0];
                    if (view != null)
                    {
                        if (isUpdating)
                        {
                            TextBoxGradBook.IsEnabled = false;
                        }
                        TextBoxGradBook.Text = view.GradebookNumber;
                        TextBoxName.Text = view.Name;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicStudent.CreateOrUpdate(new StudentBindingModel
                {
                    GradebookNumber = TextBoxGradBook.Text,
                    Name = TextBoxName.Text,
                    DenearyLogin = login
                }, isUpdating);
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
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
