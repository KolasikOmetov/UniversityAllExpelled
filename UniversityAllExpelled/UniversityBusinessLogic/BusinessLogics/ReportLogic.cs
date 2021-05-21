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
        private readonly ISubjectStorage _subjectStorage;
        public ReportLogic(ILectorStorage lectorStorage, ICheckListStorage checkListStorage, IStudentStorage studentStorage, ISubjectStorage subjectStorage)
        {
            _lectorStorage = lectorStorage;
            _checkListStorage = checkListStorage;
            _studentStorage = studentStorage;
            _subjectStorage = subjectStorage;
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
            return _checkListStorage.GetBySubject(model.DateFrom, model.DateTo, model.SubjectId);
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
        public void SaveCheckListsByDateBySubjectToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Отчёт по дисциплине",
                CheckLists = GetCheckLists(new ReportBindingModel
                {
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    SubjectId = model.SubjectId
                }),
                SubjectName = _subjectStorage.GetElement(new SubjectBindingModel
                {
                    Id = model.SubjectId
                }).Name,
                DateFrom = (DateTime)model.DateFrom,
                DateTo = (DateTime)model.DateTo,
            });
        }
    }
}
