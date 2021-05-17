using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.Services.DataServices {
	public class AccountDataService : IAccountDataService {
		private readonly IAuthenticator _authenticator;
		private readonly TaskManagementAccountsHttpClient _accountsHttpClient;
		private readonly IRenavigator _loginRenavigator;

		public AccountDataService(IAuthenticator authenticator, TaskManagementAccountsHttpClient accountsHttpClient, IRenavigator loginRenavigator) {
			_authenticator = authenticator;
			_accountsHttpClient = accountsHttpClient;
			_loginRenavigator = loginRenavigator;
		}

		public async Task<Account> GetAsync() {
			try {
				Account account = await _accountsHttpClient.GetAccountAsync(_authenticator.GetAccess());
				return account;
			}
			catch (UnauthorizedException) {
				bool refreshResult = await _authenticator.RefreshAccessToken();
				if (refreshResult == false) {
					_loginRenavigator.Renavigate();
					return null;
				}
				Account account = await _accountsHttpClient.GetAccountAsync(_authenticator.GetAccess());
				if(account == null) {
					_loginRenavigator.Renavigate();
				}
				return account;
			}
			catch (Exception) {
				_loginRenavigator.Renavigate();
				return null;
			}
		}
	}
}
