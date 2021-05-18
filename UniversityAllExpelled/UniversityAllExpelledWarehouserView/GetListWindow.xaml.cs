using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для GetListWindow.xaml
    /// </summary>
    public partial class GetListWindow : Window
    {
        private readonly LectorLogic _lectorLogic;
        private readonly StudentLogic _studentLogic;
        private readonly ReportLogic _reportLogic;
        public GetListWindow(LectorLogic lectorLogic, StudentLogic studentLogic, ReportLogic reportLogic)
        {
            InitializeComponent();
            _lectorLogic = lectorLogic;
            _studentLogic = studentLogic;
            _reportLogic = reportLogic;
        }

        private void GetListWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                ComboBoxLectors.ItemsSource = _lectorLogic.Read(null);
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

        private void ComboBoxLectors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListBoxStudent.ItemsSource = _studentLogic.SelectByLector(new LectorBindingModel { Id = (ComboBoxLectors.SelectedItem as LectorViewModel).Id });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonWord_Click(object sender, RoutedEventArgs e)
        {
            if(ComboBoxLectors.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            if ((bool)dialog.ShowDialog())
            {
                try
                {
                    _reportLogic.SaveLectorStudentsToWordFile(new ReportBindingModel {
                        FileName = dialog.FileName,
                        LectorId = (ComboBoxLectors.SelectedItem as LectorViewModel).Id
                    }, ListBoxStudent.ItemsSource as List<StudentViewModel>);
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxLectors.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите преподавателя", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if ((bool)dialog.ShowDialog())
            {
                try
                {
                    _reportLogic.SaveLectorStudentToExcelFile(new ReportBindingModel
                    {
                        FileName = dialog.FileName,
                        LectorId = (ComboBoxLectors.SelectedItem as LectorViewModel).Id
                    }, ListBoxStudent.ItemsSource as List<StudentViewModel>);
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
