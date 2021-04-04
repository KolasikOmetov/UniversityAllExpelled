﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniversityDatabaseImplement.Models
{
	public class Subject
	{
		public int? Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string DepartmentUserLogin { get; set; }
		[ForeignKey("SubjectId")]
		public virtual List<StudentSubject> StudentSubjects { get; set; }
	}
}