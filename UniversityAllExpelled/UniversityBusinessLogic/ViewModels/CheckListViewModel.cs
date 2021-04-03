using System;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class CheckListViewModel
	{
		public int Id { get; set; }
		[DisplayName("Дата экзамена")]
		public DateTime DateOfExam { get; set; }
		public int LectorId { get; set; }
	}
}
