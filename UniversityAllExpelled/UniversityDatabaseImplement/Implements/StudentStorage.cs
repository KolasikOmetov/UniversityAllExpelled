﻿using Microsoft.EntityFrameworkCore;
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
                  .ThenInclude(rec => rec.EducationPlan)
                  .Select(rec => new StudentViewModel
                  {
                      GradebookNumber = rec.GradebookNumber,
                      Name = rec.Name,
                      Subjects = rec.StudentSubjects
                      .ToDictionary(recSS => recSS.SubjectId, recSS => recSS.Subject.Name),
                      EducationPlans = rec.EducationPlanStudents
                      .ToDictionary(recES => recES.EducationPlanId, recES => recES.EducationPlan.StreamName)

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
                  .Where(rec => rec.Name == model.Name)
                  .Select(rec => new StudentViewModel
                  {
                      GradebookNumber = rec.GradebookNumber,
                      Name = rec.Name,
                      Subjects = rec.StudentSubjects
                      .ToDictionary(recSS => recSS.SubjectId, recSS => recSS.Subject.Name),
                      EducationPlans = rec.EducationPlanStudents
                      .ToDictionary(recES => recES.EducationPlanId, recES => recES.EducationPlan.StreamName)
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
                  .FirstOrDefault(rec => rec.Name == model.Name || rec.GradebookNumber == model.GradebookNumber);
                return student != null ?
                  new StudentViewModel
                  {
                      GradebookNumber = student.GradebookNumber,
                      Name = student.Name,
                      Subjects = student.StudentSubjects
                      .ToDictionary(recSS => recSS.SubjectId, recSS => recSS.Subject.Name),
                      EducationPlans = student.EducationPlanStudents
                      .ToDictionary(recES => recES.EducationPlanId, recES => recES.EducationPlan.StreamName)
                  } :
                  null;
            }
        }
        public void Insert(StudentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Student s = new Student
                        {
                            Name = model.Name,
                        };
                        context.Students.Add(s);
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
        public void Update(StudentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var element = context.Students
                          .Include(rec => rec.StudentSubjects)
                          .ThenInclude(rec => rec.Subject)
                          .Include(rec => rec.EducationPlanStudents)
                          .ThenInclude(rec => rec.EducationPlan).FirstOrDefault(rec => rec.GradebookNumber == model.GradebookNumber);
                        if (element == null)
                        {
                            throw new Exception("Элемент не найден");
                        }
                        element.Name = model.Name;
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
            // код изменён из-за ошибки вставки в бд, поэтому нужно передавать↑ уже с заполнеными полями и добавленным таблицу Students  
            if (string.IsNullOrEmpty(model.GradebookNumber))
            {
                var studentSubjects = context.StudentSubjects.Where(rec => rec.GradebookNumber == model.GradebookNumber).ToList();
                // удалили те, которых нет в модели
                context.StudentSubjects.RemoveRange(studentSubjects.Where(rec => !model.Subjects.ContainsKey(rec.SubjectId)).ToList());
                var educationPlanStudents = context.EducationPlanStudents.Where(rec => rec.GradebookNumber == model.GradebookNumber).ToList();
               
                context.EducationPlanStudents.RemoveRange(educationPlanStudents.Where(rec => !model.EducationPlans.ContainsKey(rec.EducationPlanId)).ToList());
                context.SaveChanges();
            }
            // добавили новые
            foreach (var ss in model.Subjects)
            {
                context.StudentSubjects.Add(new StudentSubject
                {
                    GradebookNumber = student.GradebookNumber,
                    SubjectId = ss.Key,
                });
                context.SaveChanges();
            }
            foreach (var ss in model.Subjects)
            {
                context.StudentSubjects.Add(new StudentSubject
                {
                    GradebookNumber = student.GradebookNumber,
                    SubjectId = ss.Key,
                });
                context.SaveChanges();
            }
            return student;
        }
    }
}