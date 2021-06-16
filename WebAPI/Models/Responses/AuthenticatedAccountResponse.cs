namespace WebAPI.Models.Responses {
	//this class is used to represent  response with tokens
	public class AuthenticatedAccountResponse {
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }

		public AuthenticatedAccountResponse(string accessToken, string refreshToken) {
			AccessToken = accessToken;
			RefreshToken = refreshToken;
		}
	}
}
