using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace UniversityBusinessLogic.ViewModels
{
    public class DenearyViewModel
    {
		[DisplayName("Логин")]
		public string Login { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
		[DisplayName("Email")]
		public string Email { get; set; }
		[DisplayName("Пароль")]
		public string Password { get; set; }
	}
}
