using System.Windows;
using Unity;

namespace UniversityAllExpelledWorkerView
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
