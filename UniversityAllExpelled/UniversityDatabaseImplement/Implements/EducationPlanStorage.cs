using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class EducationPlanStorage : IEducationPlanStorage
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        EducationPlan s = new EducationPlan
                        {
                            Id = (int)model.Id,
                            StreamName = model.StreamName,
                            Hours = model.Hours,
                            
                        };
                        model.Lectors = new Dictionary<int, string>();
                        model.Students = new Dictionary<int, string>();
                        context.EducationPlans.Add(s);
                        context.SaveChanges();
                        CreateModel(model, s, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
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
        //private EducationPlan CreateModel(EducationPlanBindingModel model, EducationPlan EducationPlan)
        //{
        //    EducationPlan.StreamName = model.StreamName;
        //    EducationPlan.Hours = model.Hours;
        //    return EducationPlan;
        //}
        private EducationPlan CreateModel(EducationPlanBindingModel model, EducationPlan ep, UniversityDatabase context)
        { 
            if (model.Id == null)
            {
                var educationPlanStudents = context.EducationPlanStudents.Where(rec => rec.GradebookNumber == model.GradebookNumber).ToList();
                context.StudentSubjects.RemoveRange(educationPlanStudents.Where(rec => !model.Subjects.ContainsKey(rec.SubjectId)).ToList());
                var educationPlanLectors = context.EducationPlanLectors.Where(rec => rec.EducationPlanId == model.Id).ToList();

                context.EducationPlanLectors.RemoveRange(educationPlanLectors.Where(rec => !model.EducationPlans.ContainsKey(rec.EducationPlanId)).ToList());
                context.SaveChanges();
            }
            foreach (var ss in model.Subjects)
            {
                context.StudentSubjects.Add(new StudentSubject
                {
                    GradebookNumber = ep.GradebookNumber,
                    SubjectId = ss.Key,
                });
                context.SaveChanges();
            }
            foreach (var ss in model.Subjects)
            {
                context.StudentSubjects.Add(new StudentSubject
                {
                    GradebookNumber = ep.GradebookNumber,
                    SubjectId = ss.Key,
                });
                context.SaveChanges();
            }
            return ep;
        }
    }
}
