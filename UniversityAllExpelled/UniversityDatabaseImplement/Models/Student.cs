using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
	public class Student
	{
		[Key]
		public string GradebookNumber { get; set; }
		[Required]
		public string Name { get; set; }
		[ForeignKey("GradebookNumber")]
		public virtual List<StudentSubject> StudentSubjects { get; set; }
		[ForeignKey("GradebookNumber")]
		public virtual List<EducationPlanStudent> EducationPlanStudents { get; set; }
	}
}
