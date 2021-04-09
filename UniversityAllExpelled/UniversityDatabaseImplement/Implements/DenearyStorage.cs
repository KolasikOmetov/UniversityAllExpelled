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
    public class DenearyStorage : IDenearyStorage
    {
        public List<DenearyViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Denearies
                .Select(rec => new DenearyViewModel
                {
                    Login = rec.Login,
                    Name = rec.Name,
                    Password = rec.Password
                }).ToList();
            }
        }
        public List<DenearyViewModel> GetFilteredList(DenearyBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Denearies
                .Where(rec => rec.Name == model.Name)
                .Select(rec => new DenearyViewModel
                {
                    Login = rec.Login,
                    Name = rec.Name,
                    Password = rec.Password
                })
                .ToList();
            }
        }
        public DenearyViewModel GetElement(DenearyBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var Deneary = context.Denearies
                .FirstOrDefault(rec => rec.Name == model.Name || rec.Login == model.Login);
                return Deneary != null ?
                new DenearyViewModel
                {
                    Login = Deneary.Login,
                    Name = Deneary.Name,
                    Password = Deneary.Password,
                } :
                null;
            }
        }
        public void Insert(DenearyBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Denearies.Add(CreateModel(model, new Deneary()));
                context.SaveChanges();
            }
        }
        public void Update(DenearyBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.Denearies.FirstOrDefault(rec => rec.Login == model.Login);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(DenearyBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Deneary element = context.Denearies.FirstOrDefault(rec => rec.Login == model.Login);
                if (element != null)
                {
                    context.Denearies.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Deneary CreateModel(DenearyBindingModel model, Deneary Deneary)
        {
            Deneary.Name = model.Name;
            Deneary.Password = model.Password;
            Deneary.Login = model.Login;
            return Deneary;
        }
    }
}
