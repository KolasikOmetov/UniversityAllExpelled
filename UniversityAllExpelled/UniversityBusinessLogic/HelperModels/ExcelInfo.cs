using UniversityBusinessLogic.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace UniversityBusinessLogic.HelperModels
{
    class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string LectorName { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }
}
