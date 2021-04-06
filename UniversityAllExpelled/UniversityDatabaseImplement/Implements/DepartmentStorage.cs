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
    public class DepartmentStorage : IDepartmentStorage
    {
        public List<DepartmentViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.Departments
                .Select(rec => new DepartmentViewModel
                {
                    Login = rec.DepartmentLogin,
                    Name = rec.Name,
                    Password = rec.Password
                }).ToList();
            }
        }
        public List<DepartmentViewModel> GetFilteredList(DepartmentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.Departments
                .Where(rec => rec.Name == model.Name)
                .Select(rec => new DepartmentViewModel
                {
                    Login = rec.DepartmentLogin,
                    Name = rec.Name,
                    Password = rec.Password
                })
                .ToList();
            }
        }
        public DepartmentViewModel GetElement(DepartmentBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var department = context.Departments
                .FirstOrDefault(rec => rec.Name == model.Name || rec.DepartmentLogin == model.DepartmentLogin);
                return department != null ?
                new DepartmentViewModel
                {
                    Login = department.DepartmentLogin,
                    Name = department.Name,
                    Password = department.Password,
                } :
                null;
            }
        }
        public void Insert(DepartmentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.Departments.Add(CreateModel(model, new Department()));
                context.SaveChanges();
            }
        }
        public void Update(DepartmentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.Departments.FirstOrDefault(rec => rec.DepartmentLogin == model.DepartmentLogin);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(DepartmentBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                Department element = context.Departments.FirstOrDefault(rec => rec.DepartmentLogin == model.DepartmentLogin);
                if (element != null)
                {
                    context.Departments.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private Department CreateModel(DepartmentBindingModel model, Department department)
        {
            department.Name = model.Name;
            department.Password = model.Password;
            department.DepartmentLogin = model.DepartmentLogin;
            return department;
        }
    }
}
