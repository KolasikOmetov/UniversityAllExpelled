using System;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class CertificationViewModel
    {
        public int Id { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Студент")]
        public string StudentName { get; set; }
    }
}
