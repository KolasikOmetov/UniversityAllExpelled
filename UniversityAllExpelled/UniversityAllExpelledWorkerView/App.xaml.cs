using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Unity;
using Unity.Lifetime;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.Interfaces;
using UniversityDatabaseImplement.Implements;

namespace UniversityAllExpelledWorkerView
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

			var enterWindow = currentContainer.Resolve<EnterWindow>();
			enterWindow.Show();
		}

		private static IUnityContainer BuildUnityContainer()
		{
			var currentContainer = new UnityContainer();
			currentContainer.RegisterType<IStudentStorage, StudentStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ICertificationStorage, CertificationStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IEducationPlanStorage, EducationPlanStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<IDenearyStorage, DenearyStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ILectorStorage, LectorStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ISubjectStorage, SubjectStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<ICheckListStorage, CheckListStorage>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<StudentLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<CertificationLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<EducationPlanLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<DenearyLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<LectorLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<SubjectLogic>(new HierarchicalLifetimeManager());
			currentContainer.RegisterType<CheckListLogic>(new HierarchicalLifetimeManager());

			return currentContainer;
		}
	}
}
