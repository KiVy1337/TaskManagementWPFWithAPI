using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Responses;

namespace WebAPI.Services.ControllerServices {
	//This service contains validation and some logic for IssueController to make it thinner
	public class IssueServiceForController : IIssueServiceForController {
		private readonly IDataService<Issue> _issueDataService;
		public IssueServiceForController(IDataService<Issue> issueDataService) {
			_issueDataService = issueDataService;
		}

		public async Task<ActionResult<Issue>> GetAsync(string accountId, int id) {
			if (!Int32.TryParse(accountId, out int userId)) {
				return new UnauthorizedResult();
			}
			Issue issue = await _issueDataService.GetAsync(id);

			if (issue == null) {
				return new NotFoundResult();
			}

			if (userId != issue.AccountId) {
				return new BadRequestResult();
			}

			return new OkObjectResult(issue);
		}

		public async Task<ActionResult<Issue>> PostAsync(string accountId, Issue issue) {
			if (!Int32.TryParse(accountId, out int userId)) {
				return new UnauthorizedResult();
			}

			if (!ChechOutIssue(issue, userId))
				return new BadRequestResult();

			await _issueDataService.CreateAsync(issue);
			return new OkObjectResult(issue);
		}

		public async Task<ActionResult<Issue>> PutAsync(string accountId, int id, Issue issue) {
			if (!Int32.TryParse(accountId, out int userId)) {
				return new UnauthorizedResult();
			}

			if (!ChechOutIssue(issue, userId))
				return new BadRequestResult();


			if (id != issue.Id) {
				return new BadRequestResult();
			}

			if (!await _issueDataService.IsExistsAsync(issue.Id)) {
				return new NotFoundResult();
			}

			return new OkObjectResult(await _issueDataService.UpdateAsync(issue));
		}

		public async Task<ActionResult> DeleteRangeAsync(string accountId, List<Issue> issues) {
			if (!Int32.TryParse(accountId, out int userId)) {
				return new UnauthorizedResult();
			}

			for (int i = 0; i < issues.Count; i++) {
				if (issues[i].AccountId != userId) {
					return new BadRequestResult();
				}
			}

			try {
				await _issueDataService.DeleteRangeAsync(issues);
			}
			catch (Exception ex) {
				return new ConflictObjectResult(new ErrorResponse("Delete request was failed."));
			}
			return new OkResult();
		}

		public bool ChechOutIssue(Issue issue, int accountId) {
			if (issue == null) {
				return false;
			}

			if (accountId != issue.AccountId) {
				return false;
			}

			return true;

		}
	}
}
