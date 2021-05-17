using System.Threading.Tasks;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Models;
using TaskManagement.WPF.Services;
using TaskManagement.WPF.Services.SaveTokens;
using TaskManagement.WPF.State.Authenticators.Responses;

namespace TaskManagement.WPF.State.Authenticators {
	//class that contains current account and can login and register help with IAuthenticationService 
	public class Authenticator : ObservableObject, IAuthenticator {
		private readonly TaskManagementAuthenticationHttpClient _authenticationClient;
		private readonly TaskManagementAccountsHttpClient _accountClient;
		private readonly ITokenProviderService _tokenProvider;
		private Account _currentAccount;
		private AuthenticatedAccountResponse Tokens;

		public Authenticator(TaskManagementAuthenticationHttpClient authenticationClient, ITokenProviderService tokenProvider, TaskManagementAccountsHttpClient accountClient) {
			_authenticationClient = authenticationClient;
			_tokenProvider = tokenProvider;
			_accountClient = accountClient;
		}

		public Account CurrentAccount {
			get {
				return _currentAccount;
			}
			private set {
				_currentAccount = value;
				OnPropertyChanged(nameof(CurrentAccount));
				OnPropertyChanged(nameof(IsLoggedIn));
			}
		}

		public bool IsLoggedIn => CurrentAccount != null;

		public async System.Threading.Tasks.Task Login(string username, string password) {
			
			Tokens = await _authenticationClient.LoginAsync(username, password);
			_tokenProvider.WriteTokensToFile(Tokens);
			CurrentAccount = await _accountClient.GetAccountAsync(Tokens.AccessToken);
		}

		public async Task<bool> TryToAuthenticateAsync() {
			Tokens = _tokenProvider.GetTokensFromFile();
			if (Tokens == null) {
				return false;
			}
			else {
				try {
					CurrentAccount = await _accountClient.GetAccountAsync(Tokens.AccessToken);
					if (CurrentAccount == null) {
						return false;
					}
				}
				catch (UnauthorizedException) {
					return await RefreshAccessToken();
				}
				return true;
			}
		}

		public async Task<bool> RefreshAccessToken() {
			Tokens = await _authenticationClient.RefreshAsync(Tokens.RefreshToken);
			if (Tokens == null) {
				return false;
			}
			else {
				CurrentAccount = await _accountClient.GetAccountAsync(Tokens.AccessToken);
				if (CurrentAccount == null) {
					return false;
				}
				else {
					_tokenProvider.WriteTokensToFile(Tokens);
					return true;
				}
			}
		}

		public async System.Threading.Tasks.Task LogoutAsync() {
			bool result = await _authenticationClient.LogoutAsync(Tokens.AccessToken);
			if (!result) {
				Tokens = await _authenticationClient.RefreshAsync(Tokens.RefreshToken);
				result = await _authenticationClient.LogoutAsync(Tokens.AccessToken);
			}
			_tokenProvider.DeleteFile();
			CurrentAccount = null;
		}

		public async Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword) {
			RegistrationResponse response =  await _authenticationClient.RegisterAsync(email, username, password, confirmPassword);
			return response.registrationResult;
		}

		public string GetAccess() {
			return Tokens.AccessToken;
		}
	}
}
