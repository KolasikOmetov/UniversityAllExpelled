using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class ReportCertificationsSubjectsViewModel
    {
        public DateTime CertificationDate { get; set; }
        public List<SubjectViewModel> Subjects { get; set; }
    }
}
