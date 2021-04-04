using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UniversityDatabaseImplement.Models
{
    public class Certification
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public List<string> StudentGradebookNumbers { get; set; }
        [Required]
        public List<string> DenearyLogins { get; set; }
    }
}
