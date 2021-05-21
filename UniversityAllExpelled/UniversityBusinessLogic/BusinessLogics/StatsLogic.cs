using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class StatsLogic
    {
        private readonly ICheckListStorage _checkListStorage;
        
        public StatsLogic(ICheckListStorage checkListStorage)
        {
            _checkListStorage = checkListStorage;
        }
        public List<StatsViewModel> GetCheckListsWithLectors(StatsBindingModel model)
        {
            return _checkListStorage.GetByDateRange(new CheckListBindingModel { 
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            }).Select(
                cL => new StatsViewModel
                {
                    CheckListDate = cL.DateOfExam,
                    CheckListId = cL.Id,
                    ItemName = cL.LectorName,
                }
                ).ToList();
        }
        public List<StatsViewModel> GetCheckListsWithSubjets(StatsBindingModel model)
        {
            return _checkListStorage.GetByDateRangeWithSubjets(new CheckListBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            });
        }
    }
}
