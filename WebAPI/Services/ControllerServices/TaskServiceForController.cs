using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskModel = WebAPI.Models.Task;
using System.Threading.Tasks;
using WebAPI.Models;
using System;
using WebAPI.Models.Responses;

namespace WebAPI.Services.ControllerServices {
	//This service contains validation and some logic for TaskController to make it thinner
	public class TaskServiceForController : ITaskServiceForController {
		private readonly IDataService<TaskModel> _taskDataService;
		private readonly GenericDataService<Issue> _issueService;

		public TaskServiceForController(IDataService<TaskModel> taskDataService, GenericDataService<Issue> issueService) {
			_taskDataService = taskDataService;
			_issueService = issueService;
		}

		public async Task<ActionResult<TaskModel>> GetAsync(string accountId, int idIssue, int id) {
			ActionResult<Issue> result = await OneIssueLoadAsync(accountId, idIssue);

			if (result.Result.GetType() != typeof(OkObjectResult)) {
				return new BadRequestResult();
			}

			TaskModel taskModel = await _taskDataService.GetAsync(id);
			if (taskModel == null)
				return new NotFoundResult();
			if (taskModel.IssueId != idIssue) {
				return new BadRequestResult();
			}
			return new OkObjectResult(taskModel);
		}

		public async Task<ActionResult<TaskModel>> PostAsync(string accountId, int idIssue, TaskModel task) {
			ActionResult<Issue> result = await OneIssueLoadAsync(accountId, idIssue);

			if (result.Result.GetType() != typeof(OkObjectResult)) {
				return new BadRequestResult();
			}

			if (!ChechOutTask(task, idIssue))
				return new BadRequestResult();

			await _taskDataService.CreateAsync(task);
			return new OkObjectResult(task);
		}

		public async Task<ActionResult<TaskModel>> PutAsync(string accountId, int idIssue, int id, TaskModel task) {
			ActionResult<Issue> result = await OneIssueLoadAsync(accountId, idIssue);

			if (result.Result.GetType() != typeof(OkObjectResult)) {
				return new BadRequestResult();
			}

			if (!ChechOutTask(task, idIssue))
				return new BadRequestResult();

			if (id != task.Id) {
				return new BadRequestResult();
			}

			if (!await _taskDataService.IsExistsAsync(task.Id)) {
				return new NotFoundResult();
			}

			return new OkObjectResult(await _taskDataService.UpdateAsync(task));
		}

		public async Task<ActionResult> DeleteRangeAsync(string accountId, int idIssue, List<TaskModel> tasks) {
			ActionResult<Issue> result = await OneIssueLoadAsync(accountId, idIssue);

			if (result.Result.GetType() != typeof(OkObjectResult)) {
				return new BadRequestResult();
			}

			for (int i=0; i < tasks.Count; i++) {
				if(tasks[i].IssueId != idIssue) {
					return new BadRequestResult();
				}
			}

			try {
				await _taskDataService.DeleteRangeAsync(tasks);
			}
			catch (Exception ex) {
				return new ConflictObjectResult(new ErrorResponse("Delete request was failed."));
			}
			
			return new OkResult();
		}

		public bool ChechOutTask(TaskModel task, int issueId) {
			if (task == null) {
				return false;
			}

			if (issueId != task.IssueId) {
				return false;
			}

			return true;

		}

		public async Task<ActionResult<Issue>> OneIssueLoadAsync(string accountId, int id) {
			if (!Int32.TryParse(accountId, out int userId)) {
				return new UnauthorizedResult();
			}
			Issue issue = await _issueService.GetAsync(id);

			if (issue == null) {
				return new NotFoundResult();
			}

			if (userId != issue.AccountId) {
				return new BadRequestResult();
			}

			return new OkObjectResult(issue);
		}
	}
}
