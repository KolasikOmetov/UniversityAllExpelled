using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IEducationPlanStorage
    {
		List<EducationPlanViewModel> GetFullList();
		List<EducationPlanViewModel> GetFilteredList(EducationPlanBindingModel model);
		EducationPlanViewModel GetElement(EducationPlanBindingModel model);
		void Insert(EducationPlanBindingModel model);
		void Update(EducationPlanBindingModel model);
		void Delete(EducationPlanBindingModel model);
		void BindingLector(int epId, int lId);
	}
}
