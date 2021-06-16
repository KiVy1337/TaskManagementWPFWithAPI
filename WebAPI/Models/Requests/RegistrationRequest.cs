using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Requests {
	//this class is used to represent registration request
	public class RegistrationRequest {
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		[Required]
		public string ConfirmPassword { get; set; }
	}
}
