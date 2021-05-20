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
    public class CheckListStorage : ICheckListStorage
    {
        public List<CheckListViewModel> GetFullList()
        {
            using (var context = new UniversityDatabase())
            {
                return context.CheckLists
                .Select(rec => new CheckListViewModel
                {
                    Id = rec.Id,
                    DateOfExam = rec.DateOfExam,
                    LectorName = context.Lectors.FirstOrDefault(recLector => recLector.Id == rec.LectorId).Name,
                    LectorId = rec.LectorId,
                }).ToList();
            }
        }
        public List<CheckListViewModel> GetFilteredList(CheckListBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                return context.CheckLists
                .Where(rec => (!model.DateFrom.HasValue && !model.DateTo.HasValue && rec.LectorId == model.LectorId || rec.DateOfExam == model.DateOfExam) ||
                (model.DateFrom.HasValue && model.DateTo.HasValue && (rec.LectorId == model.LectorId
                || rec.DateOfExam.Date >= model.DateFrom.Value.Date && rec.DateOfExam.Date <= model.DateTo.Value.Date)))
                .ToList()
                .Select(rec => new CheckListViewModel
                {
                    Id = rec.Id,
                    DateOfExam = rec.DateOfExam,
                    LectorName = context.Lectors.FirstOrDefault(recLector => recLector.Id == rec.LectorId).Name,
                    LectorId = rec.LectorId,
                })
                .ToList();
            }
        }
        public CheckListViewModel GetElement(CheckListBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new UniversityDatabase())
            {
                var checkList = context.CheckLists
                .FirstOrDefault(rec => (rec.LectorId == model.LectorId && rec.DateOfExam == model.DateOfExam) || rec.Id == model.Id);
                return checkList != null ?
                new CheckListViewModel
                {
                    Id = checkList.Id,
                    DateOfExam = checkList.DateOfExam,
                    LectorName = context.Lectors.FirstOrDefault(recLector => recLector.Id == checkList.LectorId).Name,
                    LectorId = checkList.LectorId,
                } :
                null;
            }
        }
        public void Insert(CheckListBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                context.CheckLists.Add(CreateModel(model, new CheckList()));
                context.SaveChanges();
            }
        }
        public void Update(CheckListBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                var element = context.CheckLists.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element);
                context.SaveChanges();
            }
        }
        public void Delete(CheckListBindingModel model)
        {
            using (var context = new UniversityDatabase())
            {
                CheckList element = context.CheckLists.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.CheckLists.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        private CheckList CreateModel(CheckListBindingModel model, CheckList checkList)
        {
            checkList.DateOfExam = model.DateOfExam;
            checkList.LectorId = model.LectorId;
            return checkList;
        }

        public List<ReportCheckListViewModel> GetBySubject(DateTime? dateFrom, DateTime? dateTo, int? subjectId)
        {
            if (dateFrom.HasValue && dateTo.HasValue && subjectId.HasValue)
            {
                using (var context = new UniversityDatabase())
                {
                    return context.CheckLists
                    .Where(rec => rec.DateOfExam >= dateFrom && rec.DateOfExam <= dateTo &&
                    context.Lectors.FirstOrDefault(l => l.Id == rec.LectorId && l.SubjectId == subjectId) != null)
                    .ToList()
                    .Select(rec => new ReportCheckListViewModel
                    {
                        CheckListId = rec.Id,
                        CheckListDate = rec.DateOfExam,
                        LectorName = context.Lectors.FirstOrDefault(recLector => recLector.Id == rec.LectorId).Name,
                    })
                    .ToList();
                }
            }
            else
            {
                throw new Exception("Данные не переданы");
            }
        }
    }
}
