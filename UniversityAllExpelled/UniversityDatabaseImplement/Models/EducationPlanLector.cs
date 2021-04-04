using System;
using System.Collections.Generic;
using System.Text;

namespace UniversityDatabaseImplement.Models
{
    public class EducationPlanLector
    {
        public int Id { get; set; }
        public int LectorId { get; set; }
        public int EducationPlanId { get; set; }
        public virtual EducationPlan EducationPlan { get; set; }
        public virtual Lector Lector { get; set; }
    }
}
