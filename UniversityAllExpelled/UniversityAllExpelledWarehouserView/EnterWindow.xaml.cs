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

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для EnterWindow.xaml
    /// </summary>
    public partial class EnterWindow : Window
    {
        [Dependency]
        public IUnityContainer Container { get; set; }

        public EnterWindow()
        {
            InitializeComponent();
        }

        private void ButtonAuthorization_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<AuthorizationWindow>();
            window.ShowDialog();
        }

        private void ButtonRegistration_Click(object sender, RoutedEventArgs e)
        {
            var window = Container.Resolve<RegistrationWindow>();
            window.ShowDialog();
        }
    }
}
