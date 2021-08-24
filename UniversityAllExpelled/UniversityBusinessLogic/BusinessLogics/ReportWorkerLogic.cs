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
        private readonly ISubjectStorage _subjectStorage;
        private readonly ICertificationStorage _certificationStorage;
        private readonly IStudentStorage _studentStorage;

        public ReportWorkerLogic(ISubjectStorage subjectStorage, ICertificationStorage certificationStorage, IStudentStorage studentStorage)
        {
            _subjectStorage = subjectStorage;
            _certificationStorage = certificationStorage;
            _studentStorage = studentStorage;
        }

    }
}
