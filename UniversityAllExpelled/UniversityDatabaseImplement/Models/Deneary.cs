using System.ComponentModel.DataAnnotations;

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
        public string Email { get; internal set; }
    }
}
