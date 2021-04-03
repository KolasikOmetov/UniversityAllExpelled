using System.Collections.Generic;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
	public interface IDepartmentStorage
	{
		List<DepartmentViewModel> GetFullList();
		List<DepartmentViewModel> GetFilteredList(DepartmentBindingModel model);
		DepartmentViewModel GetElement(DepartmentBindingModel model);
		void Insert(DepartmentBindingModel model);
		void Update(DepartmentBindingModel model);
		void Delete(DepartmentBindingModel model);
	}
}
