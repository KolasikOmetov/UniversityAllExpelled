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

        public string Login { set { login = value; } }

        private string login;

        private readonly DenearyLogic _logicDeneary;
        public MainWindow(DenearyLogic logic)
        {
            InitializeComponent();
            this._logicDeneary = logic;
        }

        private void MenuItemPlans_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<EditingPlansWindow>();
            window.Login = login;
            window.ShowDialog();
        }

        private void MenuItemStudents_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<StudentsWindow>();
            //window.Id = id;
            window.ShowDialog();
        }


        private void MenuItemCertifications_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<CertificationsWindow>();
            //window.Id = id;
            window.ShowDialog();
        }

        private void MenuItemGetList_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<GetListWindow>();
            window.ShowDialog();
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<ReportWindow>();
            //window.Login = login;
            window.ShowDialog();
        }

        private void MenuBindingStudent_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<BindingStudentPlanWindow>();
            //window.Login = login;
            window.ShowDialog();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var _currentUser = _logicDeneary.Read(new DenearyBindingModel { Login = login })?[0];
            labelUser.Content = $"Деканат \"{_currentUser.Name}\"";
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
