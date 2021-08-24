using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.HelperModels
{
    public class WorkerWordExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportEducationPlanSubjectsViewModel> EducationPlanSubjects { get; set; }
    }
}
