using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class ReportEducationPlanSubjectsViewModel
    {
        public string EducationPlanName { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
    }
}
