using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
	public class Lector
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int SubjectId { get; set; }
		[ForeignKey("LectorId")]
		public virtual List<EducationPlanLector> EducationPlanLectors { get; set; }
	}
}
