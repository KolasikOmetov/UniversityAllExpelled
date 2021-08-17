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
                    //Students = rec.EducationPlanStudents.ToDictionary(recEPS => recEPS.EducationPlanId, recEPS => recEPS.EducationPlan.StreamName),
                    //Lectors = rec.EducationPlanLectors.ToDictionary(recL => recL.LectorId, recL => recL.Lector.Name)          
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
                    //Students = rec.EducationPlanStudents.ToDictionary(recEPS => recEPS.EducationPlanId.ToString(), recEPS => recEPS.EducationPlan.StreamName),
                    EducationPlanLectors = rec.EducationPlanLectors.ToDictionary(recL => recL.LectorId, recL => recL.Lector.Name)
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
                    EducationPlanStudents = ep.EducationPlanStudents.ToDictionary(recEPS => recEPS.StudentGradebookNumber.ToString(), recEPS => recEPS.EducationPlan.StreamName),
                    EducationPlanLectors = ep.EducationPlanLectors.ToDictionary(recL => recL.LectorId, recL => recL.Lector.Name)
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
                        CreateModel(model, new EducationPlan(), context);
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
        public void Update(EducationPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.EducationPlans.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
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
            ep.StreamName = model.StreamName;
            ep.Hours = model.Hours;

            if (ep.Id == 0)
            {
                context.EducationPlans.Add(ep);
                context.SaveChanges();
            }

            if (model.Id.HasValue)
            {
                var EPComponents = context.EducationPlanLectors.Where(rec =>
               rec.EducationPlanId == model.Id.Value).ToList();

                context.EducationPlanLectors.RemoveRange(EPComponents.Where(rec =>
               !model.EducationPlanLectors.ContainsKey(rec.EducationPlanId)).ToList());

                foreach (var updateEP in EPComponents)
                {
                    model.EducationPlanLectors.Remove(updateEP.EducationPlanId);
                }
                context.SaveChanges();

            }
            //добавили новые
            foreach (var epl in model.EducationPlanLectors)
            {
                context.EducationPlanLectors.Add(new EducationPlanLector
                {
                    EducationPlanId = ep.Id,
                    LectorId = epl.Key,
                });
                context.SaveChanges();
            }
            return ep;
        }
    }
}
