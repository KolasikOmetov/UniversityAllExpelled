using System.Collections.Generic;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class StudentViewModel
	{
		[DisplayName("Номер зачётной книжки")]
		public string GradebookNumber { get; set; }
		[DisplayName("Имя")]
		public string Name { get; set; }
		public List<int> Subjects { get; set; }
	}
}
