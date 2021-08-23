using System;
using System.Collections.Generic;
using System.Linq;
using TypographyShopBusinessLogic.BusinessLogics;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.HelperModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class ReportWorkerLogic
    {
        private readonly ILectorStorage _lectorStorage;
        private readonly ICheckListStorage _checkListStorage;
        private readonly IStudentStorage _studentStorage;

        public ReportWorkerLogic(ILectorStorage lectorStorage, ICheckListStorage checkListStorage, IStudentStorage studentStorage)
        {
            _lectorStorage = lectorStorage;
            _checkListStorage = checkListStorage;
            _studentStorage = studentStorage;
        }
    }
}
