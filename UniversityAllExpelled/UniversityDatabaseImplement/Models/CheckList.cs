using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UniversityDatabaseImplement.Models
{
	public class CheckList
	{
		public int Id { get; set; }
		[Required]
		public DateTime DateOfExam { get; set; }
		[Required]
		public List<int> LectorIds { get; set; }
	}
}
