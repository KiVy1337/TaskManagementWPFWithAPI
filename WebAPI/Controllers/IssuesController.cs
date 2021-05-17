using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers {
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class IssuesController : ControllerBase {
		private readonly IDataService<Issue> _issueDataService;
		public IssuesController(IDataService<Issue> issueDataService) {
			_issueDataService = issueDataService;
		}

		/// <summary>
		/// Allows a authorized user to get his issue with tasks by id.
		/// </summary>

		/// <param name="id"></param> 
		/// <response code="200">Returns issue with tasks</response>
		/// <response code="404">Issue not found</response>

		// GET api/issues/id
		[HttpGet("{id}")]
		public async Task<ActionResult<Issue>> Get(int id) {
			Issue issue = await _issueDataService.GetAsync(id);
			if (issue == null)
				return NotFound();
			return new ObjectResult(issue);
		}

		/// <summary>
		/// Allows a authorized user create new issue.
		/// </summary>

		/// <param name="issue"></param> 
		/// <response code="400">Wrong information was sent in the body</response>
		/// <response code="200">Return the issue</response>

		// POST api/issues/
		[HttpPost]
		public async Task<ActionResult<Issue>> Post(Issue issue) {
			if (issue == null) {
				return BadRequest();
			}
			await _issueDataService.CreateAsync(issue);
			return Ok(issue);
		}

		/// <summary>
		/// Allows a authorized user update his issue.
		/// </summary>

		/// <param name="issue"></param> 
		/// <response code="400">Wrong information was sent in the body</response>
		/// <response code="404">Issue not found</response>
		/// <response code="200">Return the updated issue</response>

		// PUT api/issues/
		[HttpPut]
		public async Task<ActionResult<Issue>> Put(Issue issue) {
			if (issue == null) {
				return BadRequest();
			}
			if (!await _issueDataService.IsExistsAsync(issue.Id)) {
				return NotFound();
			}

			return Ok(await _issueDataService.UpdateAsync(issue));
		}


		/// <summary>
		/// Allows a authorized user delete his issues.
		/// </summary>

		/// <param name="issues"></param> 
		/// <response code="200">Return nothing</response>
		/// 
		// DELETE api/issues/
		[HttpDelete]
		public async Task<ActionResult> Delete(List<Issue> issues) {
			await _issueDataService.DeleteRangeAsync(issues);
			return Ok();
		}
	}
}
