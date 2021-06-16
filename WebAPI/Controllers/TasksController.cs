using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskModel = WebAPI.Models.Task;
using Microsoft.AspNetCore.Authorization;
using WebAPI.Services.ControllerServices;
using System.Security.Claims;

namespace WebAPI.Controllers {
	[Authorize]
	[Route("api/accounts/myaccount/issues/{idIssue}/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase {
		private readonly ITaskServiceForController _taskService;
		public TasksController(ITaskServiceForController taskService) {
			_taskService = taskService;
		}

		/// <summary>
		/// Allows a authorized user to get his task by id.
		/// </summary>

		/// <param name="id"></param>
		/// <param name="idIssue"></param> 
		/// <response code="200">Returns the task</response>
		/// <response code="404">Task not found</response>

		// GET api/accounts/myaccount/issues/idIssue/tasks/id
		[HttpGet("{id}")]
		public async Task<ActionResult<TaskModel>> GetAsync(int idIssue, int id) {
			string accountId = HttpContext.User.FindFirstValue("id");
			var result = await _taskService.GetAsync(accountId, idIssue, id);
			return result;
		}

		/// <summary>
		/// Allows a authorized user to get his task by id.
		/// </summary>

		/// <param name="idIssue"></param> 
		/// <param name="task"></param> 
		/// <response code="400">Wrong information was sent in the body</response>
		/// <response code="200">Returns task</response>

		// POST api/accounts/myaccount/issues/idIssue/tasks/
		[HttpPost]
		public async Task<ActionResult<TaskModel>> PostAsync(int idIssue, TaskModel task) {
			string accountId = HttpContext.User.FindFirstValue("id");
			var result = await _taskService.PostAsync(accountId, idIssue, task);
			return result;
		}

		/// <summary>
		/// Allows a authorized user to update his task.
		/// </summary>

		/// <param name="idIssue"></param>
		/// <param name="id"></param>
		/// <param name="task"></param> 
		/// <response code="400">Wrong information was sent in the body</response>
		/// <response code="404">The task not found</response>
		/// <response code="200">Returns the updated task</response>

		// PUT api/accounts/myaccount/issues/idIssue/tasks/id
		[HttpPut("{id}")]
		public async Task<ActionResult<TaskModel>> PutAsync(int idIssue, int id, TaskModel task) {
			string accountId = HttpContext.User.FindFirstValue("id");
			var result = await _taskService.PutAsync(accountId, idIssue, id, task);
			return result;
		}

		/// <summary>
		/// Allows a authorized user to delete his tasks.
		/// </summary>

		/// <param name="idIssue"></param>
		/// <param name="tasks"></param> 
		/// <response code="200">Returns nothing</response>

		// DELETE api/accounts/myaccount/issues/idIssue/tasks/
		[HttpDelete]
		public async Task<ActionResult> DeleteAsync(int idIssue, List<TaskModel> tasks) {
			string accountId = HttpContext.User.FindFirstValue("id");
			var result = await _taskService.DeleteRangeAsync(accountId, idIssue, tasks);
			return result;
		}
	}
}
