using System.ComponentModel.DataAnnotations;

namespace WebAppEmptyToMVC.ViewModels
{
	public class LoginModel
	{
		[Required(ErrorMessage = "Incorrect login given")]
		public string LoginOrEmail { get; set; }

		[Required(ErrorMessage = "Incorrect password given")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
