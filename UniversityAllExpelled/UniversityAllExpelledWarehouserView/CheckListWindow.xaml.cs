using System;
using System.Linq;
using System.Windows;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для CheckListWindow.xaml
    /// </summary>
    public partial class CheckListWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;
        public int LectorId
        {
            get { return Convert.ToInt32((ComboBoxLector.SelectedItem as LectorViewModel).Id); }
            set
            {
                lectorId = value;
            }
        }
        private int? lectorId;

        private readonly CheckListLogic _logicCheckList;
        private readonly LectorLogic _logicLector;

        public CheckListWindow(CheckListLogic checkListLogic, LectorLogic lectorLogic)
        {
            InitializeComponent();
            this._logicCheckList = checkListLogic;
            this._logicLector = lectorLogic;
        }

        private void CheckListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxLector.ItemsSource = _logicLector.Read(null);
            if (id.HasValue)
            {
                try
                {
                    ComboBoxLector.SelectedItem = SetValue(lectorId);
                    var view = _logicCheckList.Read(new CheckListBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        DatePicker.SelectedDate = view.DateOfExam;
                        ComboBoxLector.SelectedItem = view.LectorName;
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
            if (DatePicker.SelectedDate == null)
            {
                MessageBox.Show("Заполните дату проведения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxLector.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicCheckList.CreateOrUpdate(new CheckListBindingModel
                {
                    Id = id,
                    DateOfExam = (DateTime)DatePicker.SelectedDate,
                    LectorId = LectorId
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
        private LectorViewModel SetValue(int? value)
        {
            foreach (var item in ComboBoxLector.Items)
            {
                if ((item as LectorViewModel).Id == value)
                {
                    return item as LectorViewModel;
                }
            }
            return null;
        }

    }
}
