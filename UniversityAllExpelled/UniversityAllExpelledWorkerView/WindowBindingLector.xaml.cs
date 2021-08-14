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

namespace UniversityAllExpelledWorkerView
{
    /// <summary>
    /// Логика взаимодействия для WindowBindingLector.xaml
    /// </summary>
    public partial class WindowBindingLector : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public WindowBindingLector()
        {
            InitializeComponent();
        }
    }
}
