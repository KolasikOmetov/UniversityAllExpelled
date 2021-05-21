using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows;
using System.Windows.Media;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class StatsWindow : Window
    {
        public SeriesCollection SeriesCollection { get; set; }

        public string[] BarLabels { get; set; }

        public Func<double, string> Formatter { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly StatsLogic _logic;
        public StatsWindow(StatsLogic logic)
        {
            _logic = logic;
            InitializeComponent();
        }

        private void ButtonShow_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerFrom.SelectedDate == null || datePickerTo.SelectedDate == null)
            {
                MessageBox.Show("Вы не указали дату начала или дату окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (datePickerFrom.SelectedDate >= datePickerTo.SelectedDate)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (CheckLector.IsChecked != true && CheckSubject.IsChecked != true)
            {
                MessageBox.Show("Выберите параметр, по которому будет показана статистика", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (CheckLector.IsChecked == true && CheckSubject.IsChecked == true)
            {
                MessageBox.Show("Выберите только 1 параметр, по которому будет показана статистика", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            try
            {
                List<StatsViewModel> dataSource;
                if (CheckLector.IsChecked == true)
                {
                   dataSource = _logic.GetCheckListsWithLectors(new StatsBindingModel
                   {
                       DateFrom = datePickerFrom.SelectedDate,
                       DateTo = datePickerTo.SelectedDate,
                   });
                }
                else
                {
                    dataSource = _logic.GetCheckListsWithSubjets(new StatsBindingModel
                    {
                        DateFrom = datePickerFrom.SelectedDate,
                        DateTo = datePickerTo.SelectedDate,
                    });
                }
                string[] barLabels = new string[dataSource.Count];

                ChartValues<int> values = new ChartValues<int>();
                Dictionary<string, int> dictionary = new Dictionary<string, int>();

                foreach (var data in dataSource)
                {
                    if (dictionary.ContainsKey(data.ItemName))
                    {
                        dictionary[data.ItemName] += 1;
                    }
                    else
                    {
                        dictionary.Add(data.ItemName, 1);
                    }
                }

                int i = 0;
                foreach (var d in dictionary)
                {
                    barLabels[i] = d.Key;
                    values.Add(d.Value);
                    i++;
                }

                BarLabels = barLabels;

                SeriesCollection = new SeriesCollection();

                if (values != null)
                {
                    SeriesCollection.Add(new ColumnSeries
                    {
                        Title = "Количество ведомостей за период",
                        Values = values,
                        Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0))
                    });
                }
                else
                {
                    MessageBox.Show("Ошибка!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                Formatter = value => value.ToString("N");
                DataContext = null;
                DataContext = this;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

