using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TaskManagement.Domain.Exceptions;
using TaskManagement.Domain.Models;

namespace TaskManagement.WPF.Services {
	public class TaskManagementTasksHttpClient {
		private readonly HttpClient _client;

		public TaskManagementTasksHttpClient(HttpClient client) {
			_client = client;
		}

		public async System.Threading.Tasks.Task<Task> CreateAsync(string accessToken, Task taskToCreate) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			string json = JsonConvert.SerializeObject(taskToCreate);

			var data = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.PostAsync($"{taskToCreate.IssueId}/tasks", data);
			string jsonResponse = await response.Content.ReadAsStringAsync();
			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest) {
				return null;
			}
			if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
				throw new UnauthorizedException("Access token is invalid.");
			}
			else {
				return JsonConvert.DeserializeObject<Task>(jsonResponse);
			}
		}

		public async System.Threading.Tasks.Task<int> UpdateAsync(string accessToken, Task taskToUpdate) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
			string json = JsonConvert.SerializeObject(taskToUpdate);

			var data = new StringContent(json, Encoding.UTF8, "application/json");

			HttpResponseMessage response = await _client.PutAsync($"{taskToUpdate.IssueId}/tasks/{taskToUpdate.Id}", data);
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
		public async System.Threading.Tasks.Task<bool> DeleteAsync(string accessToken, IEnumerable<Task> tasksToDelete) {
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

			var request = new HttpRequestMessage(HttpMethod.Delete, $"{tasksToDelete.First().IssueId}/tasks");

			request.Content = new StringContent(JsonConvert.SerializeObject(tasksToDelete), Encoding.UTF8, "application/json");

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
