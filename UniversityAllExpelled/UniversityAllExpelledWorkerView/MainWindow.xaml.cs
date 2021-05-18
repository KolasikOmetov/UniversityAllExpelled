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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Unity;
using UniversityAllExpelledWarehouserView;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;

namespace UniversityAllExpelledWorkerView
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public string Login { set { login = value; } }

        private string login;

        private string id;

        public string Id { set { id = value; } }

        private readonly DenearyLogic _logicDeneary;
        public MainWindow(DenearyLogic logic)
        {
            InitializeComponent();
            this._logicDeneary = logic;
        }

        private void MenuItemStudents_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<StudentsWindow>();
            //window.Login = login;
            window.ShowDialog();
        }

        private void MenuItemPlans_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<EditingPlansWindow>();
            //window.Login = login;
            window.ShowDialog();
        }

        private void MenuItemCertifications_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<CertificationsWindow>();
            //window.
            window.ShowDialog();
        }

        private void MenuItemGetList_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<GetListWindow>();
            //window.Login = login;
            window.ShowDialog();
        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<ReportWindow>();
            //window.Login = login;
            window.ShowDialog();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var currentUser = _logicDeneary.Read(new DenearyBindingModel { Login = id })?[0];
            if (currentUser == null)
            {
                labelUser.Content = "Войдите или Зарегистрируйтесь";
            }
            else
            {
                    labelUser.Content = $"Деканат {currentUser.Name}";
            }
        }

    }
}
