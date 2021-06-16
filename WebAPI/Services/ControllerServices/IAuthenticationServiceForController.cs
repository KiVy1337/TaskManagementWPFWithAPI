using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading.Tasks;
using WebAPI.Models.Requests;

namespace WebAPI.Services.ControllerServices {
	public interface IAuthenticationServiceForController {
		Task<ActionResult> RegisterAsync(RegistrationRequest registrationRequest, ModelStateDictionary modelState);
		Task<ActionResult> LoginAsync(LoginRequest loginRequest, ModelStateDictionary modelState);
		Task<ActionResult> RefreshAsync(RefreshRequest refreshRequest, ModelStateDictionary modelState);
		Task<ActionResult> LogoutAsync(string id);
	}
}
