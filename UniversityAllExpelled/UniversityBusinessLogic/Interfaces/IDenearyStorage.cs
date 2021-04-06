using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IDenearyStorage
    {
		List<DenearyViewModel> GetFullList();
		List<DenearyViewModel> GetFilteredList(DenearyBindingModel model);
		DenearyViewModel GetElement(DenearyBindingModel model);
		void Insert(DenearyBindingModel model);
		void Update(DenearyBindingModel model);
		void Delete(DenearyBindingModel model);
	}
}
