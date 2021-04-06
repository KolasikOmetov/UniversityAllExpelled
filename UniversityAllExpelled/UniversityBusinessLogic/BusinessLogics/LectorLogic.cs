using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class LectorLogic
    {
        private readonly ILectorStorage _lectorStorage;
        public LectorLogic(ILectorStorage lectorStorage)
        {
            _lectorStorage = lectorStorage;
        }
        public List<LectorViewModel> Read(LectorBindingModel model)
        {
            if (model == null)
            {
                return _lectorStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<LectorViewModel> { _lectorStorage.GetElement(model) };
            }
            return _lectorStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(LectorBindingModel model)
        {
            var element = _lectorStorage.GetElement(new LectorBindingModel { 
                Name = model.Name,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть преподаватель с таким именем");
            }
            if (model.Id.HasValue)
            {
                _lectorStorage.Update(model);
            }
            else
            {
                _lectorStorage.Insert(model);
            }
        }
        public void Delete(LectorBindingModel model)
        {
            var element = _lectorStorage.GetElement(new LectorBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _lectorStorage.Delete(model);
        }
    }
}
