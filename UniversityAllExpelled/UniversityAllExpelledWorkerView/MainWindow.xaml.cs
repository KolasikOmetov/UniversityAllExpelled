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
        }

        private void MenuItemGetList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItemReport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
