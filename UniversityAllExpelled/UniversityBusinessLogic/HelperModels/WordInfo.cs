using System.Collections.Generic;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.HelperModels
{
    class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public string LectorName { get; set; }
        public List<StudentViewModel> Students { get; set; }
    }
}
