using System;
using System.Collections.Generic;
using System.Linq;
using TypographyShopBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.HelperModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class ReportLogic
    {
        private readonly ILectorStorage _lectorStorage;
        private readonly ICheckListStorage _checkListStorage;
        private readonly IStudentStorage _studentStorage;
        public ReportLogic(ILectorStorage lectorStorage, ICheckListStorage checkListStorage, IStudentStorage studentStorage)
        {
            _lectorStorage = lectorStorage;
            _checkListStorage = checkListStorage;
            _studentStorage = studentStorage;
        }
        public List<ReportSubjectStudentViewModel> GetSubjectStudent()
        {
            var students = _studentStorage.GetFullList();
            var list = new List<ReportSubjectStudentViewModel>();
            foreach (var student in students)
            {
                var record = new ReportSubjectStudentViewModel
                {
                    StudentName = student.Name,
                    Subjects = new List<string>(),
                };
                foreach (var subject in student.Subjects)
                {
                    record.Subjects.Add(subject.Value);
                }
                list.Add(record);
            }
            return list;
        }
        public List<ReportCheckListViewModel> GetCheckLists(ReportBindingModel model)
        {
            List<ReportCheckListViewModel> checkLists = new List<ReportCheckListViewModel>();
            List<LectorViewModel> lectors = _lectorStorage.GetFilteredList(new LectorBindingModel { SubjectId = (int)model.SubjectId });
            foreach (var lector in lectors)
            {
                checkLists.AddRange(_checkListStorage.GetFilteredList(new CheckListBindingModel { DateFrom = model.DateFrom, DateTo = model.DateTo, LectorId = lector.Id })
            .Select(x => new ReportCheckListViewModel
            {
                LectorName = x.LectorName,
                CheckListDate = x.DateOfExam
            })
            .ToList());
            }

            return checkLists;
        }
        public void SaveLectorStudentsToWordFile(ReportBindingModel model, List<StudentViewModel> students)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                LectorName = _lectorStorage.GetElement(new LectorBindingModel
                {
                    Id = model.LectorId
                }).Name,
                Title = "Список студентов",
                Students = students
            });
        }
        public void SaveLectorStudentToExcelFile(ReportBindingModel model, List<StudentViewModel> students)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                LectorName = _lectorStorage.GetElement(new LectorBindingModel
                {
                    Id = model.LectorId
                }).Name,
                Title = "Список студентов",
                Students = students
            });
        }

        [Obsolete]
        public void SaveCheckListsToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                CheckLists = GetCheckLists(model)
            });
        }
    }
}
