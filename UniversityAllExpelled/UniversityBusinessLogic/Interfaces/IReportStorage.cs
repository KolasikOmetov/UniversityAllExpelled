using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface IReportStorage
    {
        ReportCertificationsSubjectsViewModel GetCertificationSubjects(CertificationBindingModel model);
        List<ReportCertificationsViewModel> GetFullListCertifications(ReportCertificationBindingModel model);
    }
}
