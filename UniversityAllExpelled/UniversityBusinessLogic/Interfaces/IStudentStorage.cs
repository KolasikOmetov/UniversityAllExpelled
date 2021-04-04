using System.Collections.Generic;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
	public interface IStudentStorage
	{
		List<StudentViewModel> GetFullList();
		List<StudentViewModel> GetFilteredList(StudentBindingModel model);
		StudentViewModel GetElement(StudentBindingModel model);
		void Insert(StudentBindingModel model);
		void Update(StudentBindingModel model);
		void Delete(StudentBindingModel model);
	}
}
