using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class ReportEducationPlansViewModel
    {
        [DisplayName("Дисциплина")]
        public string SubjectName { get; set; }

        [DisplayName("Дата начала")]
        public DateTime DateStart { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateEnd { get; set; }

        [DisplayName("План")]
        public string EducationPlanName { get; set; }

        [DisplayName("ФИО студента")]
        public string StudentName { get; set; }
    }
}
