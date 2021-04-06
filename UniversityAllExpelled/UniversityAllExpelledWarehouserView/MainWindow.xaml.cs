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

        public string Id { set { id = value; } }

        private string id;

        private DepartmentLogic _logic;
        private DepartmentViewModel _currentUser;
        public MainWindow(DepartmentLogic logic)
        {
            InitializeComponent();
            this._logic = logic;
        }

        private void MenuItemSubjects_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null)
            {
                var window = Container.Resolve<SubjectsWindow>();
                // window.Id = id;
                window.ShowDialog();
            }
            else
            {
                // call ErrorWindow
            }
        }

        private void MenuItemLectors_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null)
            {
                //var window = Container.Resolve<LectorsWindow>();
                //window.Id = id;
                //window.ShowDialog();
            }
            else
            {
                // call ErrorWindow
            }
        }


        private void MenuItemCheckLists_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser != null)
            {
                //var window = Container.Resolve<CheckListsWindow>();
                //window.Id = id;
                //window.ShowDialog();
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

            }
            else
            {
                // call ErrorWindow
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var currentUser = _logic.Read(new DepartmentBindingModel { DepartmentLogin = id })?[0];
            if (currentUser == null)
            {
                labelUser.Content = "Войдите или Зарегистрируйтесь";
            }
            else
            {
                labelUser.Content = $"Кафедра {currentUser.Name}";
            }
        }
    }
}
