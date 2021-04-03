using System.Collections.Generic;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
	public interface IComponentStorage
	{
		List<ComponentViewModel> GetFullList();
		List<ComponentViewModel> GetFilteredList(ComponentBindingModel model);
		ComponentViewModel GetElement(ComponentBindingModel model);
		void Insert(ComponentBindingModel model);
		void Update(ComponentBindingModel model);
		void Delete(ComponentBindingModel model);
	}
}
