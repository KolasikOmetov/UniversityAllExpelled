using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using UniversityDatabaseImplement.Models;

namespace UniversityDatabaseImplement.Implements
{
    public class StudentStorage : IStudentStorage
    {
        public List<StudentViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Students
                  .Include(rec => rec.StudentSubjects)
                  .ThenInclude(rec => rec.Subject)
                  .Include(rec => rec.EducationPlanStudents)
                  .ThenInclude(rec => rec.EducationPlan).ToList()
                  .Select(rec => new StudentViewModel
                  {
                      GradebookNumber = rec.GradebookNumber,
                      Name = rec.Name,
                      DenearyLogin = context.Denearies.FirstOrDefault(x => x.Login == rec.DenearyLogin).Login                

                  }).ToList();
            }
        }
        public List<StudentViewModel> GetFilteredList(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Students
                  .Include(rec => rec.StudentSubjects)
                  .ThenInclude(rec => rec.Subject)
                  .Include(rec => rec.EducationPlanStudents)
                  .ThenInclude(rec => rec.EducationPlan)
                  .Where(rec => rec.DenearyLogin == model.DenearyLogin).ToList()
                  .Select(rec => new StudentViewModel
                  {
                      GradebookNumber = rec.GradebookNumber,
                      Name = rec.Name,
                      DenearyLogin = context.Denearies.FirstOrDefault(x => x.Login == model.DenearyLogin).Login,
                      Subjects = rec.StudentSubjects
                      .ToDictionary(recSS => recSS.SubjectId, recSS => recSS.Subject.Name),
                      EducationPlans = rec.EducationPlanStudents
                      .ToDictionary(recES => recES.EducationPlanId, recES => recES.EducationPlan.Name),
                  })
                  .ToList();
            }
        }
        public StudentViewModel GetElement(StudentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var student = context.Students
                  .Include(rec => rec.StudentSubjects)
                  .ThenInclude(rec => rec.Subject)
                  .Include(rec => rec.EducationPlanStudents)
                  .ThenInclude(rec => rec.EducationPlan)
                  .FirstOrDefault(rec => rec.GradebookNumber == model.GradebookNumber || rec.Name == model.Name);
                return student != null ?
                  new StudentViewModel
                  {
                      GradebookNumber = student.GradebookNumber,
                      Name = student.Name,
                      DenearyLogin = context.Denearies.FirstOrDefault(x => x.Login == student.DenearyLogin).Login,
                      Subjects = student.StudentSubjects
                      .ToDictionary(recSS => recSS.SubjectId, recSS => recSS.Subject.Name),
                      EducationPlans = student.EducationPlanStudents
                      .ToDictionary(recES => recES.EducationPlanId, recES => recES.EducationPlan.Name)
                  } :
                  null;
            }
        }
        public void Insert(StudentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Students.Add(CreateModel(model, new Student(), context));
                context.SaveChanges();
            }
        }
        public void Update(StudentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Students.FirstOrDefault(rec => rec.GradebookNumber ==
                       model.GradebookNumber);
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
        public void Delete(StudentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Student element = context.Students.FirstOrDefault(rec => rec.GradebookNumber == model.GradebookNumber);
                if (element != null)
                {
                    context.Students.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Student CreateModel(StudentBindingModel model, Student student, UniversityDatabase context)
        {
            student.GradebookNumber = model.GradebookNumber;
            student.Name = model.Name;
            student.DenearyLogin = model.DenearyLogin;

            return student;
        }

        public void BindingSubject(string gradebookNumber, int subjectId)
        {
            using (var context = new UniversityDatabase())
            {
                context.StudentSubjects.Add(new StudentSubject
                {
                    StudentGradebookNumber = gradebookNumber,
                    SubjectId = subjectId,
                });
                context.SaveChanges();
            }
        }

        public void BindingPlan(string gradebookNumber, int epId)
        {
            using (var context = new UniversityDatabase())
            {
                context.EducationPlanStudents.Add(new EducationPlanStudent
                {
                    StudentGradebookNumber = gradebookNumber,
                    EducationPlanId = epId,
                });
                context.SaveChanges();
            }
        }
        public List<StudentViewModel> GetBySubjectId(int subjectId)
        {
            using (var context = new UniversityDatabase())
            {
                return context.Students
                  .Include(rec => rec.StudentSubjects)
                  .ThenInclude(rec => rec.Subject)
                  .Include(rec => rec.EducationPlanStudents)
                  .ThenInclude(rec => rec.EducationPlan)
                  .ToList()
                  .Where(rec => rec.StudentSubjects.FirstOrDefault(ss => ss.SubjectId == subjectId) != null)
                  .Select(rec => new StudentViewModel
                  {
                      GradebookNumber = rec.GradebookNumber,
                      Name = rec.Name,
                      Subjects = rec.StudentSubjects
                      .ToDictionary(recSS => recSS.SubjectId, recSS => recSS.Subject.Name),
                      EducationPlans = rec.EducationPlanStudents
                      .ToDictionary(recES => recES.EducationPlanId, recES => recES.EducationPlan.Name)
                  })
                  .ToList();
            }
        }
    }
}