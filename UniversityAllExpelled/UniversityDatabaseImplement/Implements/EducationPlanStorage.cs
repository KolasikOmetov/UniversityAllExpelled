using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class EducationPlanStorage
    {
        public List<EducationPlanViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.EducationPlans   
                .Select(rec => new EducationPlanViewModel
                {
                    Id = rec.Id,
                    StreamName = rec.StreamName,
                    Hours = rec.Hours,
                }).ToList();
            }
        }
        public List<EducationPlanViewModel> GetFilteredList(EducationPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.EducationPlans
                .Where(rec => rec.StreamName == model.StreamName)
                .Select(rec => new EducationPlanViewModel
                {
                    Id = rec.Id,
                    StreamName = rec.StreamName,
                    Hours = rec.Hours,
                })
                .ToList();
            }
        }
        public EducationPlanViewModel GetElement(EducationPlanBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var ep = context.EducationPlans
                .FirstOrDefault(rec => rec.StreamName == model.StreamName || rec.Id == model.Id);
                return ep != null ?
                new EducationPlanViewModel
                {
                    Id = ep.Id,
                    StreamName = ep.StreamName,
                    Hours = ep.Hours
                } :
                null;
            }
        }
        public void Insert(EducationPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.EducationPlans.Add(CreateModel(model, new EducationPlan()));
                context.SaveChanges();
            }
        }
        public void Update(EducationPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.EducationPlans.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(EducationPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                EducationPlan element = context.EducationPlans.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.EducationPlans.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private EducationPlan CreateModel(EducationPlanBindingModel model, EducationPlan EducationPlan)
        {
            EducationPlan.StreamName = model.StreamName;
            EducationPlan.Hours = model.Hours;
            return EducationPlan;
        }
    }
}
