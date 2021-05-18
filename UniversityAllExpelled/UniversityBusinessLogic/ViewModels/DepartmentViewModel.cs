using System.ComponentModel;

namespace UniversityBusinessLogic.ViewModels
{
	public class DepartmentViewModel
	{
		[DisplayName("Логин")]
		public string Login { get; set; }
		[DisplayName("Email")]
		public string Email { get; set; }
		[DisplayName("Название")]
		public string Name { get; set; }
		[DisplayName("Пароль")]
		public string Password { get; set; }
	}
}
