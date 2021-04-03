using System.Collections.Generic;

namespace UniversityBusinessLogic.BindingModels
{
	public class StudentBindingModel
	{
		public string GradebookNumber { get; set; }
		public string Name { get; set; }
		public List<int> Subjects { get; set; }
	}
}
