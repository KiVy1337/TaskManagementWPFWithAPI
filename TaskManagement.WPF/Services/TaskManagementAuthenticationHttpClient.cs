using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Exceptions;
using TaskManagement.WPF.State.Authenticators.Requests;
using TaskManagement.WPF.State.Authenticators.Responses;

namespace TaskManagement.WPF.Services {
	public class TaskManagementAuthenticationHttpClient {
		private readonly HttpClient _client;

		public TaskManagementAuthenticationHttpClient(HttpClient client) {
			_client = client;
		}

		public async Task<AuthenticatedAccountResponse> LoginAsync(string username, string password) {
			LoginRequest loginRequest = new LoginRequest() {
				Username = username,
				Password = password
			};

			string json = JsonConvert.SerializeObject(loginRequest);

			var data = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.PostAsync("login", data);
			string jsonResponse = await response.Content.ReadAsStringAsync();

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
				if (jsonResponse == "User with this username wasn't found."){
					throw new UserNotFoundException(username);
				}
				if (jsonResponse == "Password is invalid.") {
					throw new InvalidPasswordException(username, password);
				}
			}

			return JsonConvert.DeserializeObject<AuthenticatedAccountResponse>(jsonResponse);

		}

		public async Task<AuthenticatedAccountResponse> RefreshAsync(string refresh) {
			RefreshRequest refreshRequest = new RefreshRequest() {
				RefreshToken = refresh
			};

			string json = JsonConvert.SerializeObject(refreshRequest);

			var data = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.PostAsync("refresh", data);
			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound) {
				return null;
			}

			string jsonResponse = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<AuthenticatedAccountResponse>(jsonResponse);

		}

		public async Task<RegistrationResponse> RegisterAsync(string email, string username, string password, string confirmPassword) {
			RegisterRequest registerRequest = new RegisterRequest() {
				Email = email,
				Username = username,
				Password = password,
				ConfirmPassword = confirmPassword
			};

			string json = JsonConvert.SerializeObject(registerRequest);

			var data = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.PostAsync("register", data);
			RegistrationResponse registrationResponse = new RegistrationResponse();
			string jsonResponse = await response.Content.ReadAsStringAsync();

			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest){
				registrationResponse.registrationResult = RegistrationResult.InvalidEmail;
				registrationResponse.authenticatedAccountResponse = null;
				return registrationResponse;
			}

			if(response.StatusCode == System.Net.HttpStatusCode.Conflict) {
				ErrorResponse errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(jsonResponse);
				string errorMessage = errorResponse.ErrorMessages.First();
				if (errorMessage == "Account with this email already exists.") {
					registrationResponse.registrationResult = RegistrationResult.EmailAlreadyExists;
				}
				if (errorMessage == "Account with this username already exists.") {
					registrationResponse.registrationResult = RegistrationResult.UsernameAlreadyExists;
				}
				registrationResponse.authenticatedAccountResponse = null;
				return registrationResponse;
			}

			AuthenticatedAccountResponse authenticatedAccountResponse = JsonConvert.DeserializeObject<AuthenticatedAccountResponse>(jsonResponse);
			registrationResponse.authenticatedAccountResponse = authenticatedAccountResponse;
			registrationResponse.registrationResult = RegistrationResult.Success;
			return registrationResponse;

		}

		public async Task<bool> LogoutAsync(string access) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access);

			HttpResponseMessage response = await _client.DeleteAsync("logout");
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
				return false;
			}
			else {
				return true;
			}
		}

	}
}
