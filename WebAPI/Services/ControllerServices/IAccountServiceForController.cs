using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.ControllerServices {
	public interface IAccountServiceForController {
		Task<ActionResult<Account>> GetAsync(string name);
	}
}
