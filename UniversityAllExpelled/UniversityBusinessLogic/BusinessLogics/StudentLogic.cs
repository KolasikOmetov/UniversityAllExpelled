using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class StudentLogic
    {
        private readonly IStudentStorage _studentStorage;
        private readonly ILectorStorage _lectorStorage;

        public StudentLogic(IStudentStorage studentStorage, ILectorStorage lectorStorage)
        {
            _studentStorage = studentStorage;
            _lectorStorage = lectorStorage;
        }

        public List<StudentViewModel> Read(StudentBindingModel model)
        {
            if (model == null)
            {
                return _studentStorage.GetFullList();
            }
            if (!string.IsNullOrEmpty(model.GradebookNumber))
            {
                return new List<StudentViewModel> { _studentStorage.GetElement(model) };
            }
            return _studentStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(StudentBindingModel model, bool isUpdating)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel { 
                GradebookNumber = model.GradebookNumber,
                Name = model.Name               
            });
            if (element != null && element.GradebookNumber != model.GradebookNumber)
            {
                throw new Exception("Уже есть такой студент");
            }
            if (!string.IsNullOrEmpty(model.GradebookNumber) && !isUpdating)
            {
                _studentStorage.Insert(model);
            }
            else
            {
                _studentStorage.Update(model);
            }
        }

        public void Delete(StudentBindingModel model)
        {
            var element = _studentStorage.GetElement(new StudentBindingModel { GradebookNumber = model.GradebookNumber });
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _studentStorage.Delete(model);
        }

        public List<StudentViewModel> SelectByLector(LectorBindingModel model)
        {
            var lector = _lectorStorage.GetElement(model);
            if (lector == null)
            {
                throw new Exception("Преподаватель не найден");
            }
            return _studentStorage.GetBySubjectId(lector.SubjectId);
        }

        public void BindingSubject(string gradebookNumber, int subjectId)
        {
            _studentStorage.BindingSubject(gradebookNumber, subjectId);
        }

        public void BindingPlan(string gradebookNumber, int id)
        {
            _studentStorage.BindingPlan(gradebookNumber, id);
        }
    }
}
