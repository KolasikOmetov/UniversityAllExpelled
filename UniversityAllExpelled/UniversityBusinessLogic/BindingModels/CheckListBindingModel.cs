using System;

namespace UniversityBusinessLogic.BindingModels
{
	public class CheckListBindingModel
	{
		public int Id { get; set; }
		public DateTime DateOfExam { get; set; }
		public int LectorId { get; set; }
	}
}
