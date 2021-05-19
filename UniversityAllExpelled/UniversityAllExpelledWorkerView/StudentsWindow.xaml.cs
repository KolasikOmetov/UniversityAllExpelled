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
    /// Логика взаимодействия для StudentsWindow.xaml
    /// </summary>
    public partial class StudentsWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public string Id { set { id = value; } }

        private string id;

        private readonly StudentLogic logic;

        public StudentsWindow(StudentLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(new StudentBindingModel { GradebookNumber = id });
                if (list != null)
                {
                    dataGrid.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private void StudentsWindow_Loaded(object sender, RoutedEventArgs e)
        //{
        //    LoadData();
        //}

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<StudentWindow>();
            window.GrdBookNum = id;
            if (window.ShowDialog().Value)
            {
                LoadData();
            }
        }
    }
}
