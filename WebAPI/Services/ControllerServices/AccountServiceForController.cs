using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Models;

//This service contains validation and some logic for AccountController to make it thinner
namespace WebAPI.Services.ControllerServices {
	public class AccountServiceForController : IAccountServiceForController {
		private readonly IAccountService _accountService;
		public AccountServiceForController(IAccountService accountService) {
			_accountService = accountService;
		}
		public async Task<ActionResult<Account>> GetAsync(string name) {
			Account account = await _accountService.GetByUsernameAsync(name);
			if (account == null)
				return new NotFoundResult();
			return new OkObjectResult(account);
		}
	}
}
