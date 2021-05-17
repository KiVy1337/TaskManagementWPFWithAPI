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
	public class IssueDataService : IIssueDataService {
		private readonly IAuthenticator _authenticator;
		private readonly TaskManagementIssuesHttpClient _issuesHttpClient;
		private readonly IRenavigator _loginRenavigator;

		public IssueDataService(IAuthenticator authenticator, TaskManagementIssuesHttpClient issuesHttpClient, IRenavigator loginRenavigator) {
			_authenticator = authenticator;
			_issuesHttpClient = issuesHttpClient;
			_loginRenavigator = loginRenavigator;
		}

		public async Task<Issue> GetAsync(int id) {
			try {
				Issue result = await _issuesHttpClient.GetAsync(_authenticator.GetAccess(), id);
				return result;
			}
			catch (UnauthorizedException) {
				bool refreshResult = await _authenticator.RefreshAccessToken();
				if (refreshResult == false) {
					_loginRenavigator.Renavigate();
					return null;
				}
				Issue result = await _issuesHttpClient.GetAsync(_authenticator.GetAccess(), id);
				return result;
			}
			catch (Exception) {
				_loginRenavigator.Renavigate();
				return null;
			}
		}

		public async Task<Issue> CreateAsync(Issue entity) {
			try {
				Issue result = await _issuesHttpClient.CreateAsync(_authenticator.GetAccess(), entity);
				return result;
			}
			catch (UnauthorizedException) {
				bool refreshResult = await _authenticator.RefreshAccessToken();
				if (refreshResult == false) {
					_loginRenavigator.Renavigate();
					return null;
				}
				Issue result = await _issuesHttpClient.CreateAsync(_authenticator.GetAccess(), entity);
				return result;
			}
			catch (Exception) {
				_loginRenavigator.Renavigate();
				return null;
			}
		}
		public async Task<int> UpdateAsync(Issue entity) {
			try {
				int result = await _issuesHttpClient.UpdateAsync(_authenticator.GetAccess(), entity);
				return result;
			}
			catch (UnauthorizedException) {
				bool refreshResult = await _authenticator.RefreshAccessToken();
				if (refreshResult == false) {
					_loginRenavigator.Renavigate();
					return -1;
				}
				int result = await _issuesHttpClient.UpdateAsync(_authenticator.GetAccess(), entity);
				return result;
			}
			catch (Exception) {
				_loginRenavigator.Renavigate();
				return -1;
			}
		}

		public async Task<bool> DeleteAsync(IEnumerable<Issue> entities) {
			try {
				return await _issuesHttpClient.DeleteAsync(_authenticator.GetAccess(), entities);
			}
			catch (UnauthorizedException) {
				bool refreshResult = await _authenticator.RefreshAccessToken();
				if (refreshResult == false) {
					_loginRenavigator.Renavigate();
					return false;
				}
				return await _issuesHttpClient.DeleteAsync(_authenticator.GetAccess(), entities);
			}
			catch (Exception) {
				_loginRenavigator.Renavigate();
				return false;
			}
		}
	}
}
