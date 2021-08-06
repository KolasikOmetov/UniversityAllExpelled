using System.Collections.Generic;

namespace UniversityBusinessLogic.BindingModels
{
	public class StudentBindingModel
	{
		public string GradebookNumber { get; set; }
		public string Name { get; set; }
		public string DenearyLogin { get; set; }
		public Dictionary<int, string> EducationPlanStudents { get; set; }
		public Dictionary<int, string> StudentSubjects { get; set; }
	}
}
