using System;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly DepartmentLogic _logicDepartment;
        public RegistrationWindow(DepartmentLogic logicDepartment)
        {
            InitializeComponent();
            this._logicDepartment = logicDepartment;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxDepartmentName.Text))
            {
                MessageBox.Show("Пустое поле 'Кафедра'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if(!(TextBoxDepartmentName.Text.Length <= 255 && TextBoxDepartmentName.Text.Length > 2))
            {
                MessageBox.Show("Название кафедры должно иметь длину не более 255 символов и не менее 2", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Пустое поле 'Логин'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!(TextBoxLogin.Text.Length <= 50 && TextBoxLogin.Text.Length > 2))
            {
                MessageBox.Show("Логин должен иметь длину не более 50 и не менее 2 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(TextBoxPassword.Password))
            {
                MessageBox.Show("Пустое поле 'Пароль'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!(TextBoxPassword.Password.Length <= 50 && TextBoxPassword.Password.Length >= 6))
            {
                MessageBox.Show("Пароль должен иметь длину не более 50 и не менее 6 символов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logicDepartment.CreateOrUpdate(new DepartmentBindingModel
                {
                    Name = TextBoxDepartmentName.Text,
                    DepartmentLogin = TextBoxLogin.Text,
                    Password = TextBoxPassword.Password,
                });
                MessageBox.Show("Поздравляем! Вы зарегистрированы.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
                var window = Container.Resolve<MainWindow>();
                window.Login = TextBoxDepartmentName.Text;
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
