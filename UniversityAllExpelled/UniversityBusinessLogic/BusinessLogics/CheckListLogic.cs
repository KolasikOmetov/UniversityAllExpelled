using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class CheckListLogic
    {
        private readonly ICheckListStorage _checkListStorage;
        public CheckListLogic(ICheckListStorage checkListStorage)
        {
            _checkListStorage = checkListStorage;
        }
        public List<CheckListViewModel> Read(CheckListBindingModel model)
        {
            if (model == null)
            {
                return _checkListStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CheckListViewModel> { _checkListStorage.GetElement(model) };
            }
            return _checkListStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(CheckListBindingModel model)
        {
            var element = _checkListStorage.GetElement(new CheckListBindingModel {
                DateOfExam = model.DateOfExam,
                LectorId = model.LectorId,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть ведомость с этим преподавателем и той же датой");
            }
            if (model.Id.HasValue)
            {
                _checkListStorage.Update(model);
            }
            else
            {
                _checkListStorage.Insert(model);
            }
        }
        public void Delete(CheckListBindingModel model)
        {
            var element = _checkListStorage.GetElement(new CheckListBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _checkListStorage.Delete(model);
        }
    }
}
