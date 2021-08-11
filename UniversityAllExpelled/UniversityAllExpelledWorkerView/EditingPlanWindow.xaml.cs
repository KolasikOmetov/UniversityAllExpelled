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
    /// Логика взаимодействия для EditingPlanWindow.xaml
    /// </summary>
    public partial class EditingPlanWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;

        private readonly EducationPlanLogic _logicEP;
        private readonly LectorLogic _logicL;

        private List<LectorViewModel> listAllLectors = new List<LectorViewModel>();

        public EditingPlanWindow(EducationPlanLogic logicEP, LectorLogic logicL)
        {
            InitializeComponent();
            this._logicEP = logicEP;
            this._logicL = logicL;
        }

        private void EditingPlanWindow_Loaded(object sender, RoutedEventArgs e)
        {
            listAllLectors = _logicL.Read(null);

            ListBoxLectors.ItemsSource = listAllLectors;

            if (id.HasValue)
            {
                try
                {
                    var view = _logicEP.Read(new EducationPlanBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxStream.Text = view.StreamName;
                        TextBoxHours.Text = view.Hours.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxStream.Text))
            {
                MessageBox.Show("Заполните поток", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(TextBoxHours.Text))
            {
                MessageBox.Show("Заполните количество часов", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicEP.CreateOrUpdate(new EducationPlanBindingModel
                {
                    Id = id,
                    StreamName = TextBoxStream.Text,
                    Hours = int.Parse(TextBoxHours.Text)                    
                });

                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void buttonToSelectedLectors_Click(object sender, RoutedEventArgs e)
        {
            if(ListBoxLectors.SelectedItem == null)
            {
                return;
            }
            var currentIndex = ListBoxLectors.SelectedIndex;
            ListBoxSelectedLectors.Items.Add(ListBoxLectors.SelectedItem as LectorViewModel);
            if (listAllLectors != null)
            {
                listAllLectors.RemoveAt(currentIndex);
            }
            LoadLectors();
        }

        private void buttonToLectors_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxSelectedLectors.SelectedItem == null)
            {
                return;
            }
            listAllLectors.Add(ListBoxSelectedLectors.SelectedItem as LectorViewModel);
            ListBoxSelectedLectors.Items.RemoveAt(ListBoxSelectedLectors.Items.IndexOf(ListBoxSelectedLectors.SelectedItem));
            LoadLectors();
        }

        private void LoadLectors()
        {
            ListBoxLectors.ItemsSource = null;
            ListBoxLectors.ItemsSource = listAllLectors;
        }
    }
}
