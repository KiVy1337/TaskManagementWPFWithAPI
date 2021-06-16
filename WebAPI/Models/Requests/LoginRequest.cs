using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Requests {
	//this class is used to represent login request 
	public class LoginRequest {

		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
