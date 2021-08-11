using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.BindingModels
{
    public class EducationPlanBindingModel
    {
        public int? Id { get; set; }

        public string StreamName { get; set; }

        public int Hours { get; set; }

        public Dictionary<int, string> EducationPlanLectors { get; set; }

        public Dictionary<string, string> EducationPlanStudents { get; set; }

    }
}
