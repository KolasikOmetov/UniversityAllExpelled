using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWorkerView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;

        public string Login { set { login = value; } }

        private string login;

        private DenearyLogic _logic;
        private DenearyViewModel _currentUser;
        public MainWindow(DenearyLogic logic)
        {
            InitializeComponent();
            this._logic = logic;
        }

        private void MenuItemPlans_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null)
            {
                var window = Container.Resolve<EditingPlansWindow>();
                window.Id = (int)id;
                window.ShowDialog();
            }
            else
            {
                // call ErrorWindow
            }
        }

        private void MenuItemStudents_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null)
            {
                var window = Container.Resolve<StudentsWindow>();
                //window.Id = id;
                window.ShowDialog();
            }
            else
            {
                // call ErrorWindow
            }
        }


        private void MenuItemCertifications_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null)
            {
                var window = Container.Resolve<CertificationsWindow>();
                //window.Id = id;
                window.ShowDialog();
            }
            else
            {
                // call ErrorWindow
            }
        }

        private void MenuItemGetList_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null)
            {
                var window = Container.Resolve<GetListWindow>();
                window.ShowDialog();
            }
            else
            {
                // call ErrorWindow
            }
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null)
            {
                var window = Container.Resolve<ReportWindow>();
                //window.Login = login;
                window.ShowDialog();
            }
            else
            {
                // call ErrorWindow
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _currentUser = _logic.Read(new DenearyBindingModel { Login = "abc", Password = "123456", Email = "abc@email.com", Name = "abc" })?[0];
            this.id = 1;
            if (_currentUser == null)
            {
                labelUser.Content = "Войдите или Зарегистрируйтесь";
            }
            else
            {
                labelUser.Content = $"Деканат {_currentUser.Name}";
            }
        }

        private void ButtonAuth_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AuthorizationWindow>();
            window.ShowDialog();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<RegistrationWindow>();
            window.ShowDialog();
        }
    }
}
