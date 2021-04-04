using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversityDatabaseImplement.Models
{
    public class Deneary
    {
		[Key]
		public string Login { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public List<int> CertificationIds { get; set; }
	}
}
