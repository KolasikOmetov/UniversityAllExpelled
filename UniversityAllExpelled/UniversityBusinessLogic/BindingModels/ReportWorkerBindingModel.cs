using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class ReportWorkerBindingModel
    {
        public string FileName { get; set; }
        //public int? SubjectId { get; set; }
        public int? PlanId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
