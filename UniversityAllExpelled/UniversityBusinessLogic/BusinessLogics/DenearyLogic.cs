using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class DenearyLogic
    {
        private readonly IDenearyStorage _denearyStorage;
        public DenearyLogic(IDenearyStorage denearyStorage)
        {
            _denearyStorage = denearyStorage;
        }
        public List<DenearyViewModel> Read(DenearyBindingModel model)
        {
            if (model == null)
            {
                return _denearyStorage.GetFullList();
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                return new List<DenearyViewModel> { _denearyStorage.GetElement(model) };
            }
            return _denearyStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DenearyBindingModel model)
        {
            var element = _denearyStorage.GetElement(new DenearyBindingModel
            {
                Login = model.Login
            });
            if (element != null && element.Login != model.Login)
            {
                throw new Exception("Уже есть такой логин");
            }
            if (string.IsNullOrEmpty(model.Login))
            {
                _denearyStorage.Update(model);
            }
            else
            {
                _denearyStorage.Insert(model);
            }
        }
        public void Delete(DenearyBindingModel model)
        {
            var element = _denearyStorage.GetElement(new DenearyBindingModel { Login = model.Login });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _denearyStorage.Delete(model);
        }
    }
}
