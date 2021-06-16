using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Models;

namespace TaskManagement.WPF.Services {
	public class TaskManagementAccountsHttpClient {
		private readonly HttpClient _client;

		public TaskManagementAccountsHttpClient(HttpClient client) {
			_client = client;
		}

		public async Task<Account> GetAccountAsync(string accessToken) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			HttpResponseMessage response = await _client.GetAsync("myaccount");
			if (response.StatusCode == System.Net.HttpStatusCode.NotFound){
				return null;
			}
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
				throw new UnauthorizedException("Access token is invalid.");
			}
			else {
				string jsonResponse = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<Account>(jsonResponse);
			}
		}
	}
}
