using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.ControllerServices {
	public interface IIssueServiceForController {
		Task<ActionResult<Issue>> GetAsync(string accountId, int id);
		Task<ActionResult<Issue>> PostAsync(string accountId, Issue issue);
		Task<ActionResult<Issue>> PutAsync(string accountId, int id, Issue issue);
		Task<ActionResult> DeleteRangeAsync(string accountId, List<Issue> issues);
	}
}
