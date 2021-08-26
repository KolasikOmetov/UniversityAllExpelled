using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWorkerView
{
    /// <summary>
    /// Логика взаимодействия для GetListWindow.xaml
    /// </summary>
    public partial class GetListWindow : Window
    {
        private readonly EducationPlanLogic _epLogic;
        private readonly ReportWorkerLogic _reportLogic;
        public GetListWindow(EducationPlanLogic epLogic, ReportWorkerLogic reportLogic)
        {
            InitializeComponent();
            _epLogic = epLogic;
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
                var list = _epLogic.Read(null);

                if (list != null)
                {
                    ListBoxEducationPlans.ItemsSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Question);
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ButtonSaveToExcel_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxEducationPlans.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите планы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SaveFileDialog dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" };
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    var list = new List<EducationPlanViewModel>();

                    foreach (var ep in ListBoxEducationPlans.SelectedItems)
                    {
                        list.Add((EducationPlanViewModel)ep);
                    }

                    _reportLogic.SaveEducationPlanSubjectsToExcel(new ReportEducationPlanBindingModel
                    {
                        FileName = dialog.FileName,
                        EducationPlans = list
                    });

                    MessageBox.Show("Список успешно сохранён в Excel-файл!", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonSaveToWord_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxEducationPlans.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите планы", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var dialog = new SaveFileDialog { Filter = "docx|*.docx" };
            try
            {
                if (dialog.ShowDialog() == true)
                {
                    var list = new List<EducationPlanViewModel>();

                    foreach (var ep in ListBoxEducationPlans.SelectedItems)
                    {
                        list.Add((EducationPlanViewModel)ep);
                    }

                    _reportLogic.SaveEducationPlanSubjectsToWord(new ReportEducationPlanBindingModel
                    {
                        FileName = dialog.FileName,
                        EducationPlans = list
                    });

                    MessageBox.Show("Список успешно сохранён в Word-файл!", "Успех", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
    
