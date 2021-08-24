using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.HelperModels
{
    public class WorkerPdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportEducationPlansViewModel> EducationPlansStudentsSubjects { get; set; }
    }
}
