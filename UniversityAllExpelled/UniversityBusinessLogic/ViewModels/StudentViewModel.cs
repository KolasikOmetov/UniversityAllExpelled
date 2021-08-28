﻿using System.Collections.Generic;
using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class StudentViewModel
	{
		[DisplayName("Номер зачётной книжки")]
		public string GradebookNumber { get; set; }
		[DisplayName("ФИО")]
		public string Name { get; set; }
		[DisplayName("Деканат")]
		public string DenearyLogin { get; set; }
		public Dictionary<int, string> Subjects { get; set; }
		public Dictionary<int, string> EducationPlans { get; set; }
	}
}
