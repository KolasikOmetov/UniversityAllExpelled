using System.Collections.Generic;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
	public interface ICheckListStorage
	{
		List<CheckListViewModel> GetFullList();
		List<CheckListViewModel> GetFilteredList(CheckListBindingModel model);
		CheckListViewModel GetElement(CheckListBindingModel model);
		void Insert(CheckListBindingModel model);
		void Update(CheckListBindingModel model);
		void Delete(CheckListBindingModel model);
	}
}
