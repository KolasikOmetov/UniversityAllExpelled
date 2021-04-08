using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Unity;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.ViewModels;

namespace UniversityAllExpelledWarehouserView
{
    /// <summary>
    /// Логика взаимодействия для SubjectsWindow.xaml
    /// </summary>
    public partial class SubjectsWindow : Window
    {
		[Dependency]
		public IUnityContainer Container { get; set; }
		public string Login { set { login = value; } }

		private string login;

		private readonly SubjectLogic logic;
		public SubjectsWindow(SubjectLogic logic)
		{
			InitializeComponent();
			this.logic = logic;
		}
		private void SubjectsWindow_Loaded(object sender, RoutedEventArgs e)
		{
			LoadData();
		}
		private void LoadData()
		{
			try
			{
				var list = logic.Read(new SubjectBindingModel { DepartmentLogin = login });
				if (list != null)
				{
					DataGridView.ItemsSource = list;
					DataGridView.Columns[0].Visibility = Visibility.Hidden;
					DataGridView.Columns[2].Visibility = Visibility.Hidden;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
			var window = Container.Resolve<SubjectWindow>();
			window.Login = login;
			if (window.ShowDialog().Value)
			{
				LoadData();
			}
		}
		private void ButtonUpd_Click(object sender, RoutedEventArgs e)
		{
			if (DataGridView.Items.Count == 1)
			{
				var window = Container.Resolve<SubjectWindow>();
				window.Id = ((SubjectViewModel)DataGridView.Items[0]).Id;
				if (window.ShowDialog().Value)
				{
					LoadData();
				}
			}
		}
		private void ButtonDel_Click(object sender, RoutedEventArgs e)
		{
			if (DataGridView.Items.Count == 1)
			{
				if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					int id = ((SubjectViewModel)DataGridView.Items[0]).Id;
					try
					{
						logic.Delete(new SubjectBindingModel { Id = id });
					}
					catch (Exception ex)
					{
						MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
					}
					LoadData();
				}
			}
		}

		private void ButtonRef_Click(object sender, RoutedEventArgs e)
		{
			LoadData();
		}

		/// <summary>
		/// Данные для привязки DisplayName к названиям столбцов
		/// </summary>
		private void DataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			string displayName = GetPropertyDisplayName(e.PropertyDescriptor);
			if (!string.IsNullOrEmpty(displayName))
			{
				e.Column.Header = displayName;
			}
		}

		/// <summary>
		/// метод привязки DisplayName к названию столбца
		/// </summary>
		public static string GetPropertyDisplayName(object descriptor)
		{
			PropertyDescriptor pd = descriptor as PropertyDescriptor;
			if (pd != null)
			{
				// Check for DisplayName attribute and set the column header accordingly
				DisplayNameAttribute displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;
				if (displayName != null && displayName != DisplayNameAttribute.Default)
				{
					return displayName.DisplayName;
				}
			}
			else
			{
				PropertyInfo pi = descriptor as PropertyInfo;
				if (pi != null)
				{
					// Check for DisplayName attribute and set the column header accordingly
					Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
					for (int i = 0; i < attributes.Length; ++i)
					{
						DisplayNameAttribute displayName = attributes[i] as DisplayNameAttribute;
						if (displayName != null && displayName != DisplayNameAttribute.Default)
						{
							return displayName.DisplayName;
						}
					}
				}
			}
			return null;
		}
	}
}
