using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Services;
using TaskModel = WebAPI.Models.Task;
using WebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers {
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class TasksController : ControllerBase {
		private readonly IDataService<TaskModel> _taskDataService;
		public TasksController(IDataService<TaskModel> taskDataService) {
			_taskDataService = taskDataService;
		}

		/// <summary>
		/// Allows a authorized user to get his task by id.
		/// </summary>

		/// <param name="id"></param> 
		/// <response code="200">Returns the task</response>
		/// <response code="404">Task not found</response>

		// GET api/tasks/id
		[HttpGet("{id}")]
		public async Task<ActionResult<TaskModel>> Get(int id) {
			TaskModel taskModel = await _taskDataService.GetAsync(id);
			if (taskModel == null)
				return NotFound();
			return new ObjectResult(taskModel);
		}

		/// <summary>
		/// Allows a authorized user to get his task by id.
		/// </summary>

		/// <param name="task"></param> 
		/// <response code="400">Wrong information was sent in the body</response>
		/// <response code="200">Returns task</response>

		// POST api/tasks/
		[HttpPost]
		public async Task<ActionResult<TaskModel>> Post(TaskModel task) {
			if (task == null) {
				return BadRequest();
			}
			await _taskDataService.CreateAsync(task);
			return Ok(task);
		}

		/// <summary>
		/// Allows a authorized user to update his task.
		/// </summary>

		/// <param name="task"></param> 
		/// <response code="400">Wrong information was sent in the body</response>
		/// <response code="404">The task not found</response>
		/// <response code="200">Returns the updated task</response>

		// PUT api/tasks/
		[HttpPut]
		public async Task<ActionResult<TaskModel>> Put(TaskModel task) {
			if (task == null) {
				return BadRequest();
			}
			if (!await _taskDataService.IsExistsAsync(task.Id)) {
				return NotFound();
			}

			return Ok(await _taskDataService.UpdateAsync(task));
		}

		/// <summary>
		/// Allows a authorized user to delete his tasks.
		/// </summary>

		/// <param name="tasks"></param> 
		/// <response code="200">Returns nothing</response>

		// DELETE api/tasks/
		[HttpDelete]
		public async Task<ActionResult> Delete(List<TaskModel> tasks) {
			await _taskDataService.DeleteRangeAsync(tasks);
			return Ok();
		}
	}
}
