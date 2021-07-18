using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class EducationPlanLogic
    {
		private readonly IEducationPlanStorage _educationPlanStorage;
		public EducationPlanLogic(IEducationPlanStorage educationPlanStorage)
		{
			_educationPlanStorage = educationPlanStorage;
		}
		public List<EducationPlanViewModel> Read(EducationPlanBindingModel model)
		{
			if (model == null)
			{
				return _educationPlanStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<EducationPlanViewModel> { _educationPlanStorage.GetElement(model) };
			}
			return _educationPlanStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(EducationPlanBindingModel model)
		{						
			if (model.Id.HasValue)
			{
				_educationPlanStorage.Insert(model);
			}
			var element = _educationPlanStorage.GetElement(new EducationPlanBindingModel
			{
				StreamName = model.StreamName,
				Hours = model.Hours
			});
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть план с такими данными");
			}
			if (model.Id.HasValue)
			{
				_educationPlanStorage.Update(model);
			}
			else
			{
				_educationPlanStorage.Insert(model);
			}
		}
		public void Delete(EducationPlanBindingModel model)
		{
			var element = _educationPlanStorage.GetElement(new EducationPlanBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_educationPlanStorage.Delete(model);
		}
	}
}
