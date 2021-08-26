using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System.Linq;

namespace UniversityDatabaseImplement.Implements
{
    public class ReportStorage : IReportStorage
    {
        public ReportEducationPlanSubjectsViewModel GetEducationPlanSubjects(EducationPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var subjects = from plan in context.EducationPlans
                             where plan.Id == model.Id
                             join epLectors in context.EducationPlanLectors
                             on plan.Id equals epLectors.EducationPlanId
                             join lectors in context.Lectors
                             on epLectors.LectorId equals lectors.Id
                             join subject in context.Subjects
                             on lectors.SubjectId equals subject.Id
                             select new SubjectViewModel
                             {
                                 Name = subject.Name
                             };
                return new ReportEducationPlanSubjectsViewModel
                {
                    EducationPlanName = model.Name,
                    Subjects = subjects.ToList()
                };
            }
        }

        public List<ReportEducationPlansViewModel> GetFullListEducationPlans(ReportEducationPlanBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var eps = from ep in context.EducationPlans
                          join epStudents in context.EducationPlanStudents
                          on ep.Id equals epStudents.EducationPlanId
                          where ep.DateStart >= model.DateFrom
                          where ep.DateEnd <= model.DateTo
                          join studentSubjects in context.StudentSubjects
                          on epStudents.StudentGradebookNumber equals studentSubjects.StudentGradebookNumber
                          join subject in context.Subjects
                          on studentSubjects.SubjectId equals subject.Id
                          join student in context.Students
                          on epStudents.StudentGradebookNumber equals student.GradebookNumber
                          select new ReportEducationPlansViewModel
                          {
                              EducationPlanName = ep.Name,
                              DateStart = ep.DateStart,
                              DateEnd = ep.DateEnd,
                              StudentName = student.Name,
                              SubjectName = subject.Name                    
                          };
                return eps.ToList();
            }
        }
    }
}
