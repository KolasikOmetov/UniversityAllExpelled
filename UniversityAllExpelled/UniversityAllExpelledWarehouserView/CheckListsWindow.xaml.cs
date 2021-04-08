﻿using System;
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
	/// Логика взаимодействия для CheckListsWindow.xaml
	/// </summary>
	public partial class CheckListsWindow : Window
	{
		[Dependency]
		public IUnityContainer Container { get; set; }

		private readonly CheckListLogic logic;
		public CheckListsWindow(CheckListLogic logic)
		{
			InitializeComponent();
			this.logic = logic;
		}
		private void CheckListsWindow_Loaded(object sender, RoutedEventArgs e)
		{
			LoadData();
		}
		private void LoadData()
		{
			try
			{
				var list = logic.Read(null);
				if (list != null)
				{
					DataGridView.ItemsSource = list;
					DataGridView.Columns[0].Visibility = Visibility.Hidden;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
			var window = Container.Resolve<CheckListWindow>();
			if (window.ShowDialog().Value)
			{
				LoadData();
			}
		}
		private void ButtonUpd_Click(object sender, RoutedEventArgs e)
		{
			if (DataGridView.SelectedCells.Count != 0)
			{
				var window = Container.Resolve<CheckListWindow>();
				window.Id = ((CheckListViewModel)DataGridView.SelectedCells[0].Item).Id;
				if (window.ShowDialog().Value)
				{
					LoadData();
				}
			}
		}
		private void ButtonDel_Click(object sender, RoutedEventArgs e)
		{
			if (DataGridView.SelectedCells.Count != 0)
			{
				if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					int id = ((CheckListViewModel)DataGridView.SelectedCells[0].Item).Id;
					try
					{
						logic.Delete(new CheckListBindingModel { Id = id });
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
			if (descriptor is PropertyDescriptor pd)
			{
				// Check for DisplayName attribute and set the column header accordingly
				if (pd.Attributes[typeof(DisplayNameAttribute)] is DisplayNameAttribute displayName && displayName != DisplayNameAttribute.Default)
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
						if (attributes[i] is DisplayNameAttribute displayName && displayName != DisplayNameAttribute.Default)
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