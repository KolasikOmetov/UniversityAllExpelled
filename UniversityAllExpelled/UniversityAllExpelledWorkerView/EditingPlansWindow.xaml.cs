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
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWorkerView
{
    /// <summary>
    /// Логика взаимодействия для EditingPlansWindow.xaml
    /// </summary>
    public partial class EditingPlansWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }
        private readonly EducationPlanLogic logic;

        public int Id { set { id = value; } }

        private int? id;
        public EditingPlansWindow()
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<EditingPlanWindow>();
            window.Id = (int)id;
            if (window.ShowDialog().Value)
            {
                LoadData();
            }
        }

        private void ButtonUpd_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlans.SelectedCells.Count != 0)
            {
                var window = Container.Resolve<EditingPlanWindow>();
                var cellInfo = DataGridPlans.SelectedCells[0];
                EducationPlanViewModel content = (EducationPlanViewModel)(cellInfo.Item);
                window.Id = content.Id;
                if (window.ShowDialog().Value)
                {
                    LoadData();
                }

            }
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridPlans.SelectedCells.Count != 0)
            {
                var result = MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo,
               MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        var cellInfo = DataGridPlans.SelectedCells[0];
                        EducationPlanViewModel content = (EducationPlanViewModel)(cellInfo.Item);
                        int id = content.Id;
                        logic.Delete(new EducationPlanBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK,
                       MessageBoxImage.Error);
                    }
                    LoadData();
                }
            }
        }

        private void ButtonRef_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {

            var list = logic.Read(new EducationPlanBindingModel
            {
                Id = id
            });
            if (list != null)
            {
                DataGridPlans.ItemsSource = list;
                DataGridPlans.Columns[0].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[0].Width = 0;
                DataGridPlans.Columns[1].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[1].Width = 0;

            }
        }
    }
}
