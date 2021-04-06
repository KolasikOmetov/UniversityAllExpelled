using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
    public class CertificationViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Студент")]
        public string StudentName { get; set; }

        [DisplayName("Деканат")]
        public string DenearyName { get; set; }
    }
}
