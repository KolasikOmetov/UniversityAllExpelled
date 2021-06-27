using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityDatabaseImplement.Models
{
    public class EducationPlanStudent
    {
        public int Id { get; set; }
        public string StudentGradebookNumber { get; set; }
        public int EducationPlanId { get; set; }
        public virtual EducationPlan EducationPlan { get; set; }
        public virtual Student Student { get; set; }
    }
}
