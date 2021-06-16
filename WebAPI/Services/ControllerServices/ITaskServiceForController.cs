using System.Collections.Generic;
using TaskModel = WebAPI.Models.Task;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Services.ControllerServices {
	public interface ITaskServiceForController {
		Task<ActionResult<TaskModel>> GetAsync(string accountId, int idIssue, int id);
		Task<ActionResult<TaskModel>> PostAsync(string accountId, int idIssue, TaskModel task);
		Task<ActionResult<TaskModel>> PutAsync(string accountId, int idIssue, int id, TaskModel task);
		Task<ActionResult> DeleteRangeAsync(string accountId, int idIssue, List<TaskModel> tasks);
	}
}
