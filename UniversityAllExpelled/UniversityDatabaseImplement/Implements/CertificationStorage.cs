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
    public class CertificationStorage : ICertificationStorage 
    {
        public List<CertificationViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Certifications.Where(rec ).ToList()
                .Select(rec => new CertificationViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date,
                    StudentName = context.Students.FirstOrDefault(x => x.GradebookNumber == rec.StudentGradebookNumber).Name,
                    StudentGradebookNumber = rec.StudentGradebookNumber
                }).ToList();
            }
        }
        public List<CertificationViewModel> GetFilteredList(CertificationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Certifications
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.StudentGradebookNumber == model.StudentGradebookNumber || rec.Date == model.Date) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && (rec.StudentGradebookNumber == model.StudentGradebookNumber
                || rec.Date.Date >= model.DateFrom.Value.Date && rec.Date.Date <= model.DateTo.Value.Date)))
                .ToList()
                .Select(rec => new CertificationViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date,
                    StudentName = context.Students.FirstOrDefault(x => x.GradebookNumber == rec.StudentGradebookNumber).Name,
                    StudentGradebookNumber = rec.StudentGradebookNumber
                })
                .ToList();
            }
        }
        public CertificationViewModel GetElement(CertificationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var cert = context.Certifications
                .FirstOrDefault(rec => (rec.StudentGradebookNumber == model.StudentGradebookNumber && rec.Date == model.Date) || rec.Id == model.Id);
                return cert != null ?
                new CertificationViewModel
                {
                    Id = cert.Id,
                    Date = cert.Date,
                    StudentName = context.Students.FirstOrDefault(recStudent => recStudent.GradebookNumber == cert.StudentGradebookNumber).Name,
                    StudentGradebookNumber = cert.StudentGradebookNumber,
                } :
                null;
            }
        }
        public void Insert(CertificationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Certifications.Add(CreateModel(model, new Certification()));
                context.SaveChanges();
            }
        }
        public void Update(CertificationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.Certifications.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(CertificationBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Certification element = context.Certifications.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Certifications.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Certification CreateModel(CertificationBindingModel model, Certification Certification)
        {
            Certification.Date = model.Date;
            Certification.StudentGradebookNumber = model.StudentGradebookNumber;
            
            return Certification;
        }

        public List<CertificationViewModel> GetByDateRange(CertificationBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Certifications
                .Where(rec => rec.Date.Date >= model.DateFrom.Value.Date && rec.Date.Date <= model.DateTo.Value.Date)
                .ToList()
                .Select(rec => new CertificationViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date,
                    StudentName = context.Students.FirstOrDefault(recStudent => recStudent.GradebookNumber == rec.StudentGradebookNumber).Name,
                    StudentGradebookNumber = rec.StudentGradebookNumber,
                })
                .ToList();
            }
        }
    }
}
