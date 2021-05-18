using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly DenearyLogic _logicDeneary;
        public RegistrationWindow(DenearyLogic logicDeneary)
        {
            InitializeComponent();
            this._logicDeneary = logicDeneary;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxDenearyName.Text))
            {
                MessageBox.Show("Пустое поле 'Деканат'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!(TextBoxDenearyName.Text.Length <= 255 && TextBoxDenearyName.Text.Length >= 2))
            {
                MessageBox.Show("Название деканата должно иметь длину не более 255 символов и не менее 2", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Пустое поле 'Логин'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!(TextBoxLogin.Text.Length <= 50 && TextBoxLogin.Text.Length >= 2))
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

            if (string.IsNullOrEmpty(TextBoxEmail.Text))
            {
                MessageBox.Show("Пустое поле 'Email'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!Regex.IsMatch(TextBoxEmail.Text, @"^[A-Za-z0-9]+(?:[._%+-])?[A-Za-z0-9._-]+[A-Za-z0-9]@[A-Za-z0-9]+(?:[.-])?[A-Za-z0-9._-]+\.[A-Za-z]{2,6}$"))
            {
                MessageBox.Show("Email невалидный", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                _logicDeneary.CreateOrUpdate(new DenearyBindingModel
                {
                    Name = TextBoxDenearyName.Text,
                    Email = TextBoxEmail.Text,
                    Login = TextBoxLogin.Text,
                    Password = TextBoxPassword.Password
                });
                MessageBox.Show("Поздравляем! Вы зарегистрированы.", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
                var window = Container.Resolve<MainWindow>();
                window.Login = TextBoxDenearyName.Text;
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
