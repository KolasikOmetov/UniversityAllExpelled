using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class EducationPlanBindingModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public int Hours { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        public Dictionary<int, string> EducationPlanLectors { get; set; }

        public Dictionary<string, string> EducationPlanStudents { get; set; }

    }
}
