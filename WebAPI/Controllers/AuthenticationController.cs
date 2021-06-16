using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Models.Requests;
using WebAPI.Models.Responses;
using WebAPI.Services;
using WebAPI.Services.Authenticators;
using WebAPI.Services.ControllerServices;
using WebAPI.Services.RefreshTokenRepositories;
using WebAPI.Services.TokenValidators;

namespace WebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase {
		private readonly IAuthenticationServiceForController _authenticationService;

		public AuthenticationController(IAuthenticationServiceForController authenticationService) {
			_authenticationService = authenticationService;
		}
		/// <summary>
		/// Allows a new user to register.
		/// </summary>

		/// <param name="registrationRequest"></param> 
		/// <response code="200">Returns access and refresh tokens in one object</response>
		/// <response code="400">Returns "Passwords don't match" or information about some parameters</response>
		/// <response code="409">Returns "Account with this email already exists." or "Account with this username already exists." messages </response>
		[HttpPost("register")]
		public async Task<ActionResult> RegisterAsync([FromBody] RegistrationRequest registrationRequest) {
			var result = await _authenticationService.RegisterAsync(registrationRequest, ModelState);

			return result;
		}

		/// <summary>
		/// Allows a user to login.
		/// </summary>

		/// <param name="loginRequest"></param> 
		/// <response code="200">Returns access and refresh tokens in one object</response>
		/// <response code="400">Returns information about wrong parameters</response>
		/// <response code="401">Returns "User with this username wasn't found." or "Password is invalid." messages </response>

		[HttpPost("login")]
		public async Task<ActionResult> LoginAsync([FromBody] LoginRequest loginRequest) {
			var result = await _authenticationService.LoginAsync(loginRequest, ModelState);

			return result;
		}

		/// <summary>
		/// Allows a user to refresh his tokens.
		/// </summary>

		/// <param name="refreshRequest"></param> 
		/// <response code="200">Returns access and refresh tokens in one object</response>
		/// <response code="400">Returns "Invalid refresh token." message</response>
		/// <response code="404">Returns "Invalid refresh token." or "Account not found." messages </response>

		[HttpPost("refresh")]
		public async Task<ActionResult> Refresh([FromBody] RefreshRequest refreshRequest) {
			var result = await _authenticationService.RefreshAsync(refreshRequest, ModelState);

			return result;
		}
		/// <summary>
		/// Allows authorized user to delete his all refresh tokens.
		/// </summary>


		/// <response code="204">Returns nothing:)</response>
		/// <response code="401">Wrong user id</response>
		[Authorize]
		[HttpDelete("logout")]
		public async Task<ActionResult> Logout() {
			string id = HttpContext.User.FindFirstValue("id");
			ActionResult result = await _authenticationService.LogoutAsync(id);

			return result;
		}

	}
}
