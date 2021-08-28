using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

        public string Login { set { login = value; } }

        private string login;
        public EditingPlansWindow(EducationPlanLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<EditingPlanWindow>();

            window.Login = login;

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
                window.Login = login;
                var record = (EducationPlanViewModel)DataGridPlans.SelectedCells[0].Item;
                window.Id = record.Id;
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
            var list = logic.Read(null);
            if (list != null)
            {
                DataGridPlans.ItemsSource = list;
                DataGridPlans.Columns[0].Visibility = Visibility.Hidden;
                (DataGridPlans.Columns[2] as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
                (DataGridPlans.Columns[3] as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
                DataGridPlans.Columns[5].Visibility = Visibility.Hidden;
                DataGridPlans.Columns[6].Visibility = Visibility.Hidden;
            }
        }

        private void EditingPlansWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// Данные для привязки DisplayName к названиям столбцов
        /// </summary>
        private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }
            new DataGridLength(1, DataGridLengthUnitType.Star);

        }
        /// <summary>
        /// метод привязки DisplayName к названию столбца
        /// </summary>
        public static string GetPropertyDisplayName(object descriptor)
        {

            PropertyDescriptor pd = descriptor as PropertyDescriptor;
            if (pd != null)
            {
                // Check for DisplayName attribute and set the column header accordingly
                DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }

            }
            else
            {
                PropertyInfo pi = descriptor as PropertyInfo;
                if (pi != null)
                {
                    // Check for DisplayName attribute and set the column header accordingly
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }
            return null;
        }
    }
}
