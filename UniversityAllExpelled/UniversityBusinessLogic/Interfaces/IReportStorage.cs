using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IReportStorage
    {
        ReportEducationPlanSubjectsViewModel GetEducationPlanSubjects(EducationPlanBindingModel model);
        List<ReportEducationPlansViewModel> GetFullListEducationPlans(ReportEducationPlanBindingModel model);
    }
}
