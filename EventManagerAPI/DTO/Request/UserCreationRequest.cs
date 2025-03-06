using System.ComponentModel.DataAnnotations;

namespace EventManagerAPI.DTO.Request
{
	public class UserCreationRequest
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Username is required")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Password is required")]
		[MinLength(3)]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "FirstName is required")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "LastName is required")]
		public string LastName { get; set; }


		[Required(ErrorMessage = "Confirm Password is required")]
		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password", ErrorMessage = "Password and confirmation password does not match, please try again!")]
		public string ConfirmPassword { get; set; }
	}
}
