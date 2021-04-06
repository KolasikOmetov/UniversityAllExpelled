using System.Windows;
using Unity;
using Unity.Lifetime;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.Interfaces;
using UniversityDatabaseImplement.Implements;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			IUnityContainer currentContainer = BuildUnityContainer();

			var mainWindow = currentContainer.Resolve<MainWindow>();
			mainWindow.Show();
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var currentContainer = new UnityContainer();
			currentContainer.RegisterType<IStudentStorage, StudentStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ISubjectStorage, SubjectStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ICheckListStorage, CheckListStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IDepartmentStorage, DepartmentStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ILectorStorage, LectorStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<StudentLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<SubjectLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<CheckListLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<DepartmentLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<LectorLogic>(new HierarchicalLifetimeManager());

			return currentContainer;
		}
	}
}
