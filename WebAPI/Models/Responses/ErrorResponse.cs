using System.Collections.Generic;

//this class is used to represent error response
namespace WebAPI.Models.Responses {
	public class ErrorResponse {
		public IEnumerable<string> ErrorMessages { get; set; }

		public ErrorResponse(string errorMessage) : this(new List<string>() { errorMessage }){

		}

		public ErrorResponse(IEnumerable<string> errorMessages) {
			ErrorMessages = errorMessages;
		}
	}
}
