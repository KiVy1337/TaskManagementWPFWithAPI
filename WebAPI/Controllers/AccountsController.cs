using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.Services.ControllerServices;

namespace WebAPI.Controllers {
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase {
		private readonly IAccountServiceForController _accountService;
		public AccountsController(IAccountServiceForController accountService) {
			_accountService = accountService;
		}
		/// <summary>
		/// Allows authorized user to get information about his account AND his ISSUES .
		/// </summary> 
		/// <response code="200">Returns user account</response>
		/// <response code="404">Account not found</response>

		// GET api/accounts/myaccount/
		[HttpGet("myaccount")]
		public async Task<ActionResult<Account>> GetAsync() {
			string username = HttpContext.User.FindFirstValue(ClaimTypes.Name);
			var result = await _accountService.GetAsync(username);
			return result;
		}
	}




}

