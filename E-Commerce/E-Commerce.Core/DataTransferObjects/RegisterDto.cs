using System.ComponentModel.DataAnnotations;

namespace E_Commerce.Core.DataTransferObjects
{
	public class RegisterDto
	{
		public string DisplayName { get; set; }
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		//[RegularExpression("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{8,}$", ErrorMessage = "Password Must contiain")]
		public string Password { get; set; }
	}
}
