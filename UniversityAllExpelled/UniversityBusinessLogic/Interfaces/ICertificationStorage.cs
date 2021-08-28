using System;
using System.Collections.Generic;
using System.Text;
using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.ViewModels;

namespace UniversityBusinessLogic.Interfaces
{
    public interface ICertificationStorage
    {
		List<CertificationViewModel> GetFullList();
		List<CertificationViewModel> GetFilteredList(CertificationBindingModel model);
		CertificationViewModel GetElement(CertificationBindingModel model);
		List<CertificationViewModel> GetByDateRange(CertificationBindingModel model);
		void Insert(CertificationBindingModel model);
		void Update(CertificationBindingModel model);
		void Delete(CertificationBindingModel model);
	}
}
