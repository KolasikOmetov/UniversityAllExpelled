using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class EducationPlanViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название потока")]
        public string StreamName { get; set; }

        [DisplayName("Количество часов")]
        public int Hours { get; set; }
    }
}
