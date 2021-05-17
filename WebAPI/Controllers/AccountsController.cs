using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services;

namespace WebAPI.Controllers {
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase {
		private readonly IAccountService _accountService;
		public AccountsController(IAccountService accountService) {
			_accountService = accountService;
		}
		/// <summary>
		/// Allows authorized user to get information about his account AND his ISSUES .
		/// </summary> 
		/// <response code="200">Returns user account</response>
		/// <response code="404">Account not found</response>

		// GET api/accounts/
		[HttpGet]
		public async Task<ActionResult<Account>> Get() {
			string username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
			Account account = await _accountService.GetByUsernameAsync(username);
			if (account == null)
				return NotFound();
			return new ObjectResult(account);
		}

		///// <summary>
		///// Allows authorized user to get information about his account .
		///// </summary> 
		///// <response code="200">Returns user account</response>
		///// <response code="404">Account not found</response>
		//// POST api/accounts/
		//[HttpPost]
		//public async Task<ActionResult<Account>> Post(Account account) {
		//	if (account == null) {
		//		return BadRequest();
		//	}
		//	await _accountService.CreateAsync(account);
		//	return Ok(account);
		//}

		//// PUT api/accounts/
		//[HttpPut]
		//public async Task<ActionResult<Account>> Put(Account account) {
		//	if (account == null) {
		//		return BadRequest();
		//	}
		//	if (! await _accountService.IsExistsAsync(account.Id)) {
		//		return NotFound();
		//	}

		//	return Ok(await _accountService.UpdateAsync(account));
		//}

		//// DELETE api/accounts/
		//[HttpDelete]
		//public async Task<ActionResult> Delete(List<Account> accounts) {
		//	await _accountService.DeleteRangeAsync(accounts);
		//	return Ok();
		//}
	}




}

