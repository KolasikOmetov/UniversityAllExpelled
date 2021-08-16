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
    /// Логика взаимодействия для WindowBindingLector.xaml
    /// </summary>
    public partial class WindowBindingLector : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id
        {
            get { return (ComboBoxLectors.SelectedItem as LectorViewModel).Id; }
            set { id = value; }
        }
        private int? id;
        private string login;

        public string Login
        {
            set { login = value; }
        }

        private readonly LectorLogic logic;

        public string LectorName { get { return (ComboBoxLectors.SelectedItem as LectorViewModel).Name; } }

        public WindowBindingLector(LectorLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            Close();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxLectors.SelectedValue == null)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK,
               MessageBoxImage.Error);
                return;
            }
            this.DialogResult = true;
            Close();
        }

        private LectorViewModel SetValue(int value)
        {
            foreach (var item in ComboBoxLectors.Items)
            {
                if ((item as LectorViewModel).Id == value)
                {
                    return item as LectorViewModel;
                }
            }
            return null;
        }

        private void WindowBindingLector_Loaded(object sender, RoutedEventArgs e)
        {
            //var list = logic.Read(new LectorBindingModel
            //{
            //    ClientId = clientId
            //});
            var list = logic.Read(null);
            if (list != null)
            {
                ComboBoxLectors.ItemsSource = list;
            }
            if (id != null)
            {
                ComboBoxLectors.SelectedItem = SetValue(id.Value);
                id = null;
            }
        }
    }
}
