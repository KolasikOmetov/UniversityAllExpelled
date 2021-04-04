using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class DepartmentLogic
    {
        private readonly IDepartmentStorage _departmentStorage;
        public DepartmentLogic(IDepartmentStorage departmentStorage)
        {
            _departmentStorage = departmentStorage;
        }
        public List<DepartmentViewModel> Read(DepartmentBindingModel model)
        {
            if (model == null)
            {
                return _departmentStorage.GetFullList();
            }
            if (!string.IsNullOrEmpty(model.Login))
            {
                return new List<DepartmentViewModel> { _departmentStorage.GetElement(model) };
            }
            return _departmentStorage.GetFilteredList(model);
        }
        public void CreateOrUpdate(DepartmentBindingModel model)
        {
            var element = _departmentStorage.GetElement(new DepartmentBindingModel { 
                Name = model.Name,
            });
            if (element != null && element.Login != model.Login)
            {
                throw new Exception("Уже есть кафедра с таким названием");
            }
            if (!string.IsNullOrEmpty(model.Login))
            {
                _departmentStorage.Update(model);
            }
            else
            {
                _departmentStorage.Insert(model);
            }
        }
        public void Delete(DepartmentBindingModel model)
        {
            var element = _departmentStorage.GetElement(new DepartmentBindingModel { Login = model.Login });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _departmentStorage.Delete(model);
        }
    }
}
