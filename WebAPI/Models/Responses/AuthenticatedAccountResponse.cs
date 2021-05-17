using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models.Responses {
	public class AuthenticatedAccountResponse {
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }

		public AuthenticatedAccountResponse(string accessToken, string refreshToken) {
			AccessToken = accessToken;
			RefreshToken = refreshToken;
		}
	}
}
