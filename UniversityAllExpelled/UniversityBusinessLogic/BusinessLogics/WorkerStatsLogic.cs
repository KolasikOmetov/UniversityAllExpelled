using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class WorkerStatsLogic
    {
        private readonly ICertificationStorage _certificationStorage;
        
        public WorkerStatsLogic(ICertificationStorage certificationStorage)
        {
            _certificationStorage = certificationStorage;
        }

        public List<WorkerStatsViewModel> GetCertificationsWithStudents(StatsBindingModel model)
        {
            return _certificationStorage.GetByDateRange(new CertificationBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).Select(
                c => new WorkerStatsViewModel
                {
                    CertificationDate = c.Date,
                    CertificationId = c.Id,
                    ItemName = c.StudentName,
                }
                ).ToList();
        }
    }
}
