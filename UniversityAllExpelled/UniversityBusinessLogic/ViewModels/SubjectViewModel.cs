using System;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class SubjectViewModel
	{
		public int Id { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
	}
}
