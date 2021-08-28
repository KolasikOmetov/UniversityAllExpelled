using LiveCharts;
using LiveCharts.Wpf;
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
    /// Логика взаимодействия для WindowStats.xaml
    /// </summary>
    public partial class WindowStats : Window
    {
        public SeriesCollection SeriesCollection { get; set; }

        public string[] BarLabels { get; set; }

        public Func<double, string> Formatter { get; set; }

        [Dependency]
        public IUnityContainer Container { get; set; }

        private readonly WorkerStatsLogic _logic;
        public WindowStats(WorkerStatsLogic logic)
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
            try
            {
                List<WorkerStatsViewModel> dataSource;

                dataSource = _logic.GetCertificationsWithStudents(new StatsBindingModel
                {
                    DateFrom = datePickerFrom.SelectedDate,
                    DateTo = datePickerTo.SelectedDate,
                });
                
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
                        Title = "Студент",
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
