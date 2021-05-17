using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.WPF.State.Authenticators.Responses {
	public class ErrorResponse {
		public IEnumerable<string> ErrorMessages { get; set; }

		public ErrorResponse(IEnumerable<string> errorMessages) {
			ErrorMessages = errorMessages;
		}
	}
}
