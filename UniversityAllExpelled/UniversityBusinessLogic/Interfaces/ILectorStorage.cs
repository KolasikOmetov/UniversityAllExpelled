using System.Collections.Generic;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
	public interface ILectorStorage
	{
		List<LectorViewModel> GetFullList();
		List<LectorViewModel> GetFilteredList(LectorBindingModel model);
		LectorViewModel GetElement(LectorBindingModel model);
		void Insert(LectorBindingModel model);
		void Update(LectorBindingModel model);
		void Delete(LectorBindingModel model);
	}
}
