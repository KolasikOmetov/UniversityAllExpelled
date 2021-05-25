using Microsoft.EntityFrameworkCore;
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
                          .Include(rec => rec.EducationPlanLectors)
                          .ThenInclude(rec => rec.Lector)
                          .Include(rec => rec.EducationPlanStudents)
                          .ThenInclude(rec => rec.Student).ToList()
                .Select(rec => new EducationPlanViewModel
                {
                    Id = rec.Id,
                    StreamName = rec.StreamName,
                    Hours = rec.Hours,
                    Students = rec.EducationPlanStudents.ToDictionary(recEPS => recEPS.EducationPlanId.ToString(), recEPS => recEPS.EducationPlan.StreamName),
                    Lectors = rec.EducationPlanLectors.ToDictionary(recL => recL.LectorId, recL => recL.Lector.Name)
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
                          .Include(rec => rec.EducationPlanLectors)
                          .ThenInclude(rec => rec.Lector)
                          .Include(rec => rec.EducationPlanStudents)
                          .ThenInclude(rec => rec.Student).ToList()
                .Where(rec => rec.StreamName == model.StreamName)
                .Select(rec => new EducationPlanViewModel
                {
                    Id = rec.Id,
                    StreamName = rec.StreamName,
                    Hours = rec.Hours,
                    Students = rec.EducationPlanStudents.ToDictionary(recEPS => recEPS.EducationPlanId.ToString(), recEPS => recEPS.EducationPlan.StreamName),
                    Lectors = rec.EducationPlanLectors.ToDictionary(recL => recL.LectorId, recL => recL.Lector.Name)
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
                          .Include(rec => rec.EducationPlanLectors)
                          .ThenInclude(rec => rec.Lector)
                          .Include(rec => rec.EducationPlanStudents)
                          .ThenInclude(rec => rec.Student)
                .FirstOrDefault(rec => rec.StreamName == model.StreamName || rec.Id == model.Id);
                return ep != null ?
                new EducationPlanViewModel
                {
                    Id = ep.Id,
                    StreamName = ep.StreamName,
                    Hours = ep.Hours,
                    Students = ep.EducationPlanStudents.ToDictionary(recEPS => recEPS.EducationPlanId.ToString(), recEPS => recEPS.EducationPlan.StreamName),
                    Lectors = ep.EducationPlanLectors.ToDictionary(recL => recL.LectorId, recL => recL.Lector.Name)
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
                            //Id = (int)model.Id,
                            StreamName = model.StreamName,
                            Hours = model.Hours,
                            
                        };
                        model.Lectors = new Dictionary<int, string>();
                        model.Students = new Dictionary<string, string>();
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.EducationPlans
                          .Include(rec => rec.EducationPlanLectors)
                          .ThenInclude(rec => rec.Lector)
                          .Include(rec => rec.EducationPlanStudents)
                          .ThenInclude(rec => rec.Student).FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.StreamName = model.StreamName;
                        element.Hours = model.Hours;
                        CreateModel(model, element, context);
                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
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

        private EducationPlan CreateModel(EducationPlanBindingModel model, EducationPlan ep, UniversityDatabase context)
        { 
            if (model.Id == null)
            {
                var educationPlanStudents = context.EducationPlanStudents.Where(rec => rec.Id == model.Id).ToList();
                context.EducationPlanStudents.RemoveRange(educationPlanStudents.Where(rec => !model.Students.ContainsKey(rec.GradebookNumber)).ToList());
                var educationPlanLectors = context.EducationPlanLectors.Where(rec => rec.EducationPlanId == model.Id).ToList();

                context.EducationPlanLectors.RemoveRange(educationPlanLectors.Where(rec => !model.Lectors.ContainsKey(rec.EducationPlanId)).ToList());
                context.SaveChanges();
            }
            foreach (var ss in model.Students)
            {
                context.EducationPlanStudents.Add(new EducationPlanStudent
                {
                    EducationPlanId = ep.Id,
                    GradebookNumber = ss.Key                  
                });
                context.SaveChanges();
            }

            return ep;
        }
    }
}
