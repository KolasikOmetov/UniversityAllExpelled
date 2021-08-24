using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BindingModels
{
    public class ReportEducationPlanBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public List<EducationPlanViewModel> EducationPlans { get; set; }
    }
}
