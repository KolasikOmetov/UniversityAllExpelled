using System;
using System.Linq;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly DepartmentLogic _logicDepartment;

        public AuthorizationWindow(DepartmentLogic logicDepartment)
        {
            InitializeComponent();
            this._logicDepartment = logicDepartment;
        }

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxLogin.Text))
            {
                MessageBox.Show("Пустое поле 'Логин'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxPassword.Password))
            {
                MessageBox.Show("Пустое поле 'Пароль'", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                var viewDepartment = _logicDepartment.Read(new DepartmentBindingModel
                {
                    DepartmentLogin = TextBoxLogin.Text,
                    Password = TextBoxPassword.Password
                });
                if (viewDepartment != null && viewDepartment.Count > 0)
                {
                    DialogResult = true;
                    var window = Container.Resolve<MainWindow>();
                    window.Login = viewDepartment[0].Login;
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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
