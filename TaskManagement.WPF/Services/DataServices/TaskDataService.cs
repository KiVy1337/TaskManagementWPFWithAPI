using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Exceptions;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.Services.DataServices {
	class TaskDataService : ITaskDataService {
		private readonly IAuthenticator _authenticator;
		private readonly TaskManagementTasksHttpClient _tasksHttpClient;
		private readonly IRenavigator _loginRenavigator;

		public TaskDataService(IAuthenticator authenticator, TaskManagementTasksHttpClient tasksHttpClient, IRenavigator loginRenavigator) {
			_authenticator = authenticator;
			_tasksHttpClient = tasksHttpClient;
			_loginRenavigator = loginRenavigator;
		}
		public async Task<Domain.Models.Task> CreateAsync(Domain.Models.Task entity) {
			try {
				Domain.Models.Task result = await _tasksHttpClient.CreateAsync(_authenticator.GetAccess(), entity);
				return result;
			}
			catch (UnauthorizedException) {
				bool refreshResult = await _authenticator.RefreshAccessToken();
				if (refreshResult == false) {
					_loginRenavigator.Renavigate();
					return null;
				}
				Domain.Models.Task result = await _tasksHttpClient.CreateAsync(_authenticator.GetAccess(), entity);
				return result;
			}
			catch (Exception) {
				_loginRenavigator.Renavigate();
				return null;
			}
		}

		public async Task<int> UpdateAsync(Domain.Models.Task entity) {
			try {
				int result = await _tasksHttpClient.UpdateAsync(_authenticator.GetAccess(), entity);
				return result;
			}
			catch (UnauthorizedException) {
				bool refreshResult = await _authenticator.RefreshAccessToken();
				if (refreshResult == false) {
					_loginRenavigator.Renavigate();
					return -1;
				}
				int result = await _tasksHttpClient.UpdateAsync(_authenticator.GetAccess(), entity);
				return result;
			}
			catch (Exception) {
				_loginRenavigator.Renavigate();
				return -1;
			}
		}
		public async Task<bool> DeleteAsync(IEnumerable<Domain.Models.Task> entities) {
			try {
				return await _tasksHttpClient.DeleteAsync(_authenticator.GetAccess(), entities);
			}
			catch (UnauthorizedException) {
				bool refreshResult = await _authenticator.RefreshAccessToken();
				if (refreshResult == false) {
					_loginRenavigator.Renavigate();
					return false;
				}
				return await _tasksHttpClient.DeleteAsync(_authenticator.GetAccess(), entities);
			}
			catch (Exception) {
				_loginRenavigator.Renavigate();
				return false;
			}
		}
	}
}
