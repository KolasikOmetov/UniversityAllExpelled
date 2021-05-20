using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWarehouserView
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

        private readonly DepartmentLogic _logicDepartment;
        public MainWindow(DepartmentLogic logic)
        {
            InitializeComponent();
            this._logicDepartment = logic;
        }

        private void MenuItemSubjects_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<SubjectsWindow>();
            window.Login = login;
            window.ShowDialog();
        }

        private void MenuItemLectors_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<LectorsWindow>();
            window.Login = login;
            window.ShowDialog();
        }


        private void MenuItemCheckLists_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<CheckListsWindow>();
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
            window.Login = login;
            window.ShowDialog();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var currentUser = _logicDepartment.Read(new DepartmentBindingModel { DepartmentLogin = login })?[0];
            labelUser.Content = $"Кафедра \"{currentUser.Name}\"";
        }

        private void MenuItemBinding_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<BindingSubjectWindow>();
            window.Login = login;
            window.ShowDialog();
        }
    }
}
