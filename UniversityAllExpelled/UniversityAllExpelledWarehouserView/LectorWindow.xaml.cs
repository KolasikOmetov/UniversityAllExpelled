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
    /// Логика взаимодействия для LectorWindow.xaml
    /// </summary>
    public partial class LectorWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private int? id;
        public int SubjectId
        {
            get { return Convert.ToInt32((ComboBoxSubject.SelectedItem as SubjectViewModel).Id); }
            set
            {
                subjectId = value;
            }
        }
        private int? subjectId;
        public string Login { set { login = value; } }

        private string login;

        private readonly LectorLogic _logicLector;
        private readonly SubjectLogic _logicSubject;

        public LectorWindow(LectorLogic lectorLogic, SubjectLogic subjectLogic)
        {
            InitializeComponent();
            this._logicLector = lectorLogic;
            this._logicSubject = subjectLogic;
        }

        private void LectorWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBoxSubject.ItemsSource = _logicSubject.Read(new SubjectBindingModel { DepartmentLogin = login });
            ComboBoxSubject.SelectedItem = SetValue(subjectId);
            if (id.HasValue)
            {
                try
                {
                    var view = _logicLector.Read(new LectorBindingModel { Id = id })?[0];
                    if (view != null)
                    {
                        TextBoxName.Text = view.Name;
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
            if (string.IsNullOrEmpty(TextBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (ComboBoxSubject.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите дисциплину", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                _logicLector.CreateOrUpdate(new LectorBindingModel
                {
                    Id = id,
                    Name = TextBoxName.Text,
                    SubjectId = SubjectId
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

        private SubjectViewModel SetValue(int? value)
        {
            foreach (var item in ComboBoxSubject.Items)
            {
                if ((item as SubjectViewModel).Id == value)
                {
                    return item as SubjectViewModel;
                }
            }
            return null;
        }
    }
}
