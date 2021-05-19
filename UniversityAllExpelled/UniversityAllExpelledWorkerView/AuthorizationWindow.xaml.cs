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
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly DenearyLogic _logicDeneary;

        public AuthorizationWindow(DenearyLogic logicDeneary)
        {
            InitializeComponent();
            this._logicDeneary = logicDeneary;
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
                var viewDeneary = _logicDeneary.Read(new DenearyBindingModel
                {
                    //Login = TextBoxLogin.Text,
                    //Password = TextBoxPassword.Password
                    Login = "abc",
                    Password = "123456"
                });
                //if (viewDeneary != null && viewDeneary[0] != null && viewDeneary.Count > 0 && viewDeneary[0].Password == TextBoxPassword.Password)
                //{
                    DialogResult = true;
                    var window = Container.Resolve<MainWindow>();
                    window.Login = "abc"; //viewDeneary[0].Login;
                    window.ShowDialog();
                //}
                //else
                //{
                //    MessageBox.Show("Неверный логин или пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                //}
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
