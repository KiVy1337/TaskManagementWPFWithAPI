using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Requests;
using WebAPI.Models.Responses;
using WebAPI.Services;
using WebAPI.Services.Authenticators;
using WebAPI.Services.RefreshTokenRepositories;
using WebAPI.Services.TokenGenerators;
using WebAPI.Services.TokenValidators;

namespace WebAPI.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase {
		private readonly IAccountService _accountService;
		private readonly IPasswordHasher _passwordHasher;
		private readonly Authenticator _authenticator;
		private readonly RefreshTokenValidator _refreshTokenValidator;
		private readonly IRefreshTokenRepository _refreshTokenRepository;

		public AuthenticationController(IAccountService accountService, IPasswordHasher passwordHasher,
			Authenticator authenticator, RefreshTokenValidator refreshTokenValidator,
			IRefreshTokenRepository refreshTokenRepository) {

			_accountService = accountService;
			_passwordHasher = passwordHasher;
			_authenticator = authenticator;
			_refreshTokenValidator = refreshTokenValidator;
			_refreshTokenRepository = refreshTokenRepository;
		}
		/// <summary>
		/// Allows a new user to register.
		/// </summary>

		/// <param name="registrationRequest"></param> 
		/// <response code="200">Returns access and refresh tokens in one object</response>
		/// <response code="400">Returns "Passwords don't match" or information about some parameters</response>
		/// <response code="409">Returns "Account with this email already exists." or "Account with this username already exists." messages </response>
		[HttpPost("register")]
		public async Task<ActionResult> Register([FromBody] RegistrationRequest registrationRequest) {
			if (!ModelState.IsValid) {
				return BadRequestModelState();
			}

			if (registrationRequest.Password != registrationRequest.ConfirmPassword) {
				return BadRequest(new ErrorResponse("Passwords don't match."));

			}

			Account existingAccountByEmail = await _accountService.GetByEmailAsync(registrationRequest.Email);
			if(existingAccountByEmail != null) {
				return Conflict(new ErrorResponse("Account with this email already exists."));
			}

			Account existingAccountByUsername = await _accountService.GetByUsernameAsync(registrationRequest.Username);
			if (existingAccountByUsername != null) {
				return Conflict(new ErrorResponse("Account with this username already exists."));
			}

			string passwordHash = _passwordHasher.HashPassword(registrationRequest.Password);
			Account registrationAccount = new Account() {
				Email = registrationRequest.Email,
				Username = registrationRequest.Username,
				PasswordHash = passwordHash,
				DatesJoined = DateTime.Today
			};

			Account account = await _accountService.CreateAsync(registrationAccount);
			AuthenticatedAccountResponse response = await _authenticator.Authenticate(account);

			return Ok(response);
		}

		/// <summary>
		/// Allows a user to login.
		/// </summary>

		/// <param name="loginRequest"></param> 
		/// <response code="200">Returns access and refresh tokens in one object</response>
		/// <response code="400">Returns information about wrong parameters</response>
		/// <response code="401">Returns "User with this username wasn't found." or "Password is invalid." messages </response>

		[HttpPost("login")]
		public async Task<ActionResult> Login([FromBody] LoginRequest loginRequest) {
			if (!ModelState.IsValid) {
				return BadRequestModelState();
			}

			Account account = await _accountService.GetByUsernameAsync(loginRequest.Username);
			if(account == null) {
				return Unauthorized("User with this username wasn't found.");
			}

			PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(account.PasswordHash, loginRequest.Password);
			if (passwordResult != PasswordVerificationResult.Success) {
				return Unauthorized("Password is invalid.");
			}
			AuthenticatedAccountResponse response = await _authenticator.Authenticate(account);

			return Ok(response);


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
			if (!ModelState.IsValid) {
				return BadRequestModelState();
			}

			bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
			if (!isValidRefreshToken) {
				return BadRequest(new ErrorResponse("Invalid refresh token."));
			}

			RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByToken(refreshRequest.RefreshToken);

			if(refreshTokenDTO == null) {
				return NotFound(new ErrorResponse("Invalid refresh token."));
			}

			await _refreshTokenRepository.Delete(refreshTokenDTO.Id);

			Account account = await _accountService.GetAsync(refreshTokenDTO.AccountId);
			if(account == null) {
				return NotFound(new ErrorResponse("Account not found."));
			}

			AuthenticatedAccountResponse response = await _authenticator.Authenticate(account);

			return Ok(response);

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
			if(!Int32.TryParse(id, out int userId)) {
				return Unauthorized();
			}
			await _refreshTokenRepository.DeleteAll(userId);

			return NoContent();


		}



		private ActionResult BadRequestModelState() {
			IEnumerable<string> errorMessages = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
			return BadRequest(new ErrorResponse(errorMessages));
		}
	}
}
