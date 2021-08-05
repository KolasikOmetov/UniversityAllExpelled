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
    public class CertificationStorage : ICertificationStorage //МЕНЯТЬ
    {
        public List<CertificationViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Certifications
                .Select(rec => new CertificationViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date,
                    StudentName = context.Students.FirstOrDefault(x => x.GradebookNumber == rec.StudentGradebookNumber).Name                    
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
                .Where(rec => rec.StudentGradebookNumber == model.StudentGradebookNumber /*&& rec.DenearyLogin == model.DenearyLogin*/)
                .Select(rec => new CertificationViewModel
                {
                    Id = rec.Id,
                    Date = rec.Date,
                    StudentName = context.Students.FirstOrDefault(x => x.GradebookNumber == rec.StudentGradebookNumber).Name,
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
                var Cert = context.Certifications
                .FirstOrDefault(rec => rec.Date == model.Date /*|| rec.DenearyLogin == model.DenearyLogin*/);
                return Cert != null ?
                new CertificationViewModel
                {
                    Id = Cert.Id,
                    Date = Cert.Date,
                    StudentName = Cert.StudentGradebookNumber
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
            //???
            return Certification;
        }
    }
}
