using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Requests {
	//this class is used to represent refresh request
	public class RefreshRequest {
		[Required]
		public string RefreshToken { get; set; }
	}
}
