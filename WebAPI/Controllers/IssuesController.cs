using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services.ControllerServices;

namespace WebAPI.Controllers {
	[Authorize]
	[Route("api/accounts/myaccount/[controller]")]
	[ApiController]
	public class IssuesController : ControllerBase {
		private readonly IIssueServiceForController _issueServiceForController;
		public IssuesController(IIssueServiceForController issueServiceForController) {
			_issueServiceForController = issueServiceForController;
		}

		/// <summary>
		/// Allows a authorized user to get his issue with tasks by id.
		/// </summary>

		/// <param name="id"></param> 
		/// <response code="200">Returns issue with tasks</response>
		/// <response code="404">Issue not found</response>

		// GET api/accounts/myaccount/issues/id
		[HttpGet("{id}")]
		public async Task<ActionResult<Issue>> GetAsync(int id) {
			string accountId = HttpContext.User.FindFirstValue("id");
			var result = await _issueServiceForController.GetAsync(accountId, id);
			return result;
		}

		/// <summary>
		/// Allows a authorized user create new issue.
		/// </summary>

		/// <param name="issue"></param> 
		/// <response code="400">Wrong information was sent in the body</response>
		/// <response code="200">Return the issue</response>

		// POST api/accounts/myaccount/issues/
		[HttpPost]
		public async Task<ActionResult<Issue>> PostAsync(Issue issue) {
			string accountId = HttpContext.User.FindFirstValue("id");
			var result = await _issueServiceForController.PostAsync(accountId, issue);
			return result;
		}

		/// <summary>
		/// Allows a authorized user update his issue.
		/// </summary>

		/// <param name="id"></param>
		/// <param name="issue"></param> 
		/// <response code="400">Wrong information was sent in the body</response>
		/// <response code="404">Issue not found</response>
		/// <response code="200">Return the updated issue</response>

		// PUT api/accounts/myaccount/issues/id
		[HttpPut("{id}")]
		public async Task<ActionResult<Issue>> PutAsync(int id, Issue issue) {
			string accountId = HttpContext.User.FindFirstValue("id");
			var result = await _issueServiceForController.PutAsync(accountId, id, issue);
			return result;
		}


		/// <summary>
		/// Allows a authorized user delete his issues.
		/// </summary>

		/// <param name="issues"></param> 
		/// <response code="200">Return nothing</response>
		/// 
		// DELETE api/accounts/myaccount/issues/
		[HttpDelete]
		public async Task<ActionResult> DeleteAsync(List<Issue> issues) {
			string accountId = HttpContext.User.FindFirstValue("id");
			var result = await _issueServiceForController.DeleteRangeAsync(accountId, issues);
			return result;
		}
	}
}
