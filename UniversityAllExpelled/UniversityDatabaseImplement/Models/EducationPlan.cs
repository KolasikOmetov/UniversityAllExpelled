using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversityDatabaseImplement.Models
{
    public class EducationPlan
    {
		public int Id { get; set; }
		[Required]
		public string StreamName { get; set; }
		[Required]
		public int Hours { get; set; }
		[ForeignKey("EducationPlanId")]
		public virtual List<EducationPlanStudent> EducationPlanStudents { get; set; }
		[ForeignKey("EducationPlanId")]
		public virtual List<EducationPlanLector> EducationPlanLectors { get; set; }
	}
}
