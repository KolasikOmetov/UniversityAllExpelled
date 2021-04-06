using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class CertificationLogic
    {
        private readonly ICertificationStorage _certificationStorage;
        public CertificationLogic(ICertificationStorage certificationStorage)
        {
            _certificationStorage = certificationStorage;
        }
        public List<CertificationViewModel> Read(CertificationBindingModel model)
        {
            if (model == null)
            {
                return _certificationStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<CertificationViewModel> { _certificationStorage.GetElement(model) };
            }
            return _certificationStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(CertificationBindingModel model)
        {
            var element = _certificationStorage.GetElement(new CertificationBindingModel
            {
                Id = model.Id
            }); 
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть студент с таким именем");
            }
            if (model.Id.HasValue)
            {
                _certificationStorage.Update(model);
            }
            else
            {
                _certificationStorage.Insert(model);
            }
        }
        public void Delete(CertificationBindingModel model)
        {
            var element = _certificationStorage.GetElement(new CertificationBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _certificationStorage.Delete(model);
        }
    }
}
