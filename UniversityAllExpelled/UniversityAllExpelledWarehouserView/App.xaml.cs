using System;
using System.Windows;
using System.Windows.Controls;
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
		public static string defaultReportPath = "D:\\Report.pdf";
		public static string emailSender = "lab7using@gmail.com";
		public static string emailHost = "smtp.gmail.com";
		public static int emailPort = 587;
		public static string emailPassword = "321ewq#@!";

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			IUnityContainer currentContainer = BuildUnityContainer();

			var enterWindow = currentContainer.Resolve<MainWindow>();
			enterWindow.Login = "ist";
			enterWindow.Show();
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
