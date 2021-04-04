using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UniversityDatabaseImplement.Models
{
    public class Certification
    {
        [Required]
        public DateTime Date { get; set; }
        [ForeignKey("GradebookNumber")]
        public virtual Student Student { get; set; }
        [ForeignKey("Login")]
        public virtual Deneary Deneary { get; set; }
    }
}
