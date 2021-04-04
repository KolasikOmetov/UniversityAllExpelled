using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class SubjectStorage : ISubjectStorage
    {
        public List<SubjectViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Subjects
                .Select(rec => new SubjectViewModel
                {
                    Id = rec.Id,
                    SubjectName = rec.SubjectName
                }).ToList();
            }
        }
        public List<SubjectViewModel> GetFilteredList(SubjectBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Subjects
                .Where(rec => rec.SubjectName.Contains(model.SubjectName))
                .Select(rec => new SubjectViewModel
                {
                    Id = rec.Id,
                    SubjectName = rec.SubjectName
                })
                .ToList();
            }
        }
        public SubjectViewModel GetElement(SubjectBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var subject = context.Subjects
                .FirstOrDefault(rec => rec.SubjectName == model.SubjectName || rec.Id == model.Id);
                return subject != null ?
                new SubjectViewModel
                {
                    Id = subject.Id,
                    Name = subject.Name,
                    DepartmentUserLogin = subject.DepartmentUserLogin
                } :
                null;
            }
        }
        public void Insert(SubjectBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Subjects.Add(CreateModel(model, new Subject()));
                context.SaveChanges();
            }
        }
        public void Update(SubjectBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.Subjects.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(SubjectBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Subject element = context.Subjects.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Subjects.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Subject CreateModel(SubjectBindingModel model, Subject subject)
        {
            subject.Name = model.Name;
            return subject;
        }
    }
}
