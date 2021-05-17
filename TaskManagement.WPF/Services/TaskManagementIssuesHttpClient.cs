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
	public class TaskManagementIssuesHttpClient {
		private readonly HttpClient _client;

		public TaskManagementIssuesHttpClient(HttpClient client) {
			_client = client;
		}

		public async Task<Issue> GetAsync(string accessToken, int id) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			HttpResponseMessage response = await _client.GetAsync($"{id}");
			string jsonResponse = await response.Content.ReadAsStringAsync();
			if (response.StatusCode == System.Net.HttpStatusCode.NotFound) {
				return null;
			}
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
				throw new UnauthorizedException("Access token is invalid.");
			}
			else {
				return JsonConvert.DeserializeObject<Issue>(jsonResponse);
			}
		}

		public async Task<Issue> CreateAsync(string accessToken, Issue issueToCreate) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			string json = JsonConvert.SerializeObject(issueToCreate);

			var data = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.PostAsync("", data);
			string jsonResponse = await response.Content.ReadAsStringAsync();
			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) {
				return null;
			}
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
				throw new UnauthorizedException("Access token is invalid.");
			}
			else {
				return JsonConvert.DeserializeObject<Issue>(jsonResponse);
			}
		}
		public async Task<int> UpdateAsync(string accessToken, Issue issueToUpdate) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			string json = JsonConvert.SerializeObject(issueToUpdate);

			var data = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.PutAsync("", data);
			string jsonResponse = await response.Content.ReadAsStringAsync();
			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest || response.StatusCode == System.Net.HttpStatusCode.NotFound) {
				return -1;
			}
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
				throw new UnauthorizedException("Access token is invalid.");
			}
			else {
				return Convert.ToInt32(jsonResponse);
			}
		}

		public async Task<bool> DeleteAsync(string accessToken, IEnumerable<Issue> issuesToDelete) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var request = new HttpRequestMessage(HttpMethod.Delete, "");

			request.Content = new StringContent(JsonConvert.SerializeObject(issuesToDelete), Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.SendAsync(request);

			string jsonResponse = await response.Content.ReadAsStringAsync();

			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
				throw new UnauthorizedException("Access token is invalid.");
			}
			else {
				return true;
			}
		}
	}
}
