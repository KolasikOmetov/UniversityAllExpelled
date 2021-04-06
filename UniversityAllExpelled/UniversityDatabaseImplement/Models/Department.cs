﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityDatabaseImplement.Models
{
	public class Department
	{
		[Key]
		public string DepartmentLogin { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
