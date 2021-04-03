using UniversityBusinessLogic.BindingModels;
using UniversityBusinessLogic.Interfaces;
using UniversityBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;

namespace UniversityBusinessLogic.BusinessLogics
{
    public class PrintedLogic
    {
		private readonly IPrintedStorage _printedStorage;
		public PrintedLogic(IPrintedStorage printedStorage)
		{
			_printedStorage = printedStorage;
		}
		public List<PrintedViewModel> Read(PrintedBindingModel model)
		{
			if (model == null)
			{
				return _printedStorage.GetFullList();
			}
			if (model.Id.HasValue)
			{
				return new List<PrintedViewModel> { _printedStorage.GetElement(model) };
			}
			return _printedStorage.GetFilteredList(model);
		}
		public void CreateOrUpdate(PrintedBindingModel model)
		{
			var element = _printedStorage.GetElement(new PrintedBindingModel { PrintedName = model.PrintedName });
			if (element != null && element.Id != model.Id)
			{
				throw new Exception("Уже есть изделие с таким названием");
			}
			if (model.Id.HasValue)
			{
				_printedStorage.Update(model);
			}
			else
			{
				_printedStorage.Insert(model);
			}
		}
		public void Delete(PrintedBindingModel model)
		{
			var element = _printedStorage.GetElement(new PrintedBindingModel { Id = model.Id });
			if (element == null)
			{
				throw new Exception("Элемент не найден");
			}
			_printedStorage.Delete(model);
		}
	}
}
