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
		[ForeignKey("StudentId")]
		public virtual List<StudentSubject> StudentSubjects { get; set; }
	}
}
