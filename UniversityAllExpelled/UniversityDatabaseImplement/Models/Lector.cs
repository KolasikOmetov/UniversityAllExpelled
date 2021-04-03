using System.ComponentModel.DataAnnotations;

namespace UniversityDatabaseImplement.Models
{
	public class Lector
	{
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int SubjectId { get; set; }
	}
}
