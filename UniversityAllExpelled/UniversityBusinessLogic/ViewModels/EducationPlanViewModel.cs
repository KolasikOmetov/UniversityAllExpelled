using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class EducationPlanViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Дата начала")]
        public DateTime DateStart { get; set; }

        [DisplayName("Дата окончания")]
        public DateTime DateEnd { get; set; }

        [DisplayName("Количество часов")]
        public int Hours { get; set; }

        public Dictionary<string, string> EducationPlanStudents { get; set; }
        public Dictionary<int, string> EducationPlanLectors { get; set; }
    }
}
