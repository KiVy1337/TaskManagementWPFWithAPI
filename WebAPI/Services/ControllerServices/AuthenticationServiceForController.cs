using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Requests;
using WebAPI.Models.Responses;
using WebAPI.Services.Authenticators;
using WebAPI.Services.RefreshTokenRepositories;
using WebAPI.Services.TokenValidators;

namespace WebAPI.Services.ControllerServices {
	//This service contains validation and some logic for AuthenticationController to make it thinner
	public class AuthenticationServiceForController : IAuthenticationServiceForController {
		private readonly IAccountService _accountService;
		private readonly IPasswordHasher _passwordHasher;
		private readonly Authenticator _authenticator;
		private readonly RefreshTokenValidator _refreshTokenValidator;
		private readonly IRefreshTokenRepository _refreshTokenRepository;


		public AuthenticationServiceForController(IAccountService accountService, IPasswordHasher passwordHasher,
			Authenticator authenticator, RefreshTokenValidator refreshTokenValidator,
			IRefreshTokenRepository refreshTokenRepository) {

			_accountService = accountService;
			_passwordHasher = passwordHasher;
			_authenticator = authenticator;
			_refreshTokenValidator = refreshTokenValidator;
			_refreshTokenRepository = refreshTokenRepository;
		}


		public async Task<ActionResult> RegisterAsync(RegistrationRequest registrationRequest, ModelStateDictionary modelState) {
			if (!modelState.IsValid) {
				return BadRequestModelState(modelState);
			}

			if (registrationRequest.Password != registrationRequest.ConfirmPassword) {
				return new BadRequestObjectResult(new ErrorResponse("Passwords don't match."));

			}

			Account existingAccountByEmail = await _accountService.GetByEmailAsync(registrationRequest.Email);
			if (existingAccountByEmail != null) {
				return new ConflictObjectResult(new ErrorResponse("Account with this email already exists."));
			}

			Account existingAccountByUsername = await _accountService.GetByUsernameAsync(registrationRequest.Username);
			if (existingAccountByUsername != null) {
				return new ConflictObjectResult(new ErrorResponse("Account with this username already exists."));
			}

			string passwordHash = _passwordHasher.HashPassword(registrationRequest.Password);
			Account registrationAccount = new Account() {
				Email = registrationRequest.Email,
				Username = registrationRequest.Username,
				PasswordHash = passwordHash,
				DatesJoined = DateTime.Today
			};

			Account account = await _accountService.CreateAsync(registrationAccount);
			AuthenticatedAccountResponse response = await _authenticator.AuthenticateAsync(account);

			return new OkObjectResult(response);
		}

		public async Task<ActionResult> LoginAsync(LoginRequest loginRequest, ModelStateDictionary modelState) {
			if (!modelState.IsValid) {
				return BadRequestModelState(modelState);
			}

			Account account = await _accountService.GetByUsernameAsync(loginRequest.Username);
			if (account == null) {
				return new UnauthorizedObjectResult("User with this username wasn't found.");
			}

			PasswordVerificationResult passwordResult = _passwordHasher.VerifyHashedPassword(account.PasswordHash, loginRequest.Password);
			if (passwordResult != PasswordVerificationResult.Success) {
				return new UnauthorizedObjectResult("Password is invalid.");
			}
			AuthenticatedAccountResponse response = await _authenticator.AuthenticateAsync(account);

			return new OkObjectResult(response);
		}

		public async Task<ActionResult> RefreshAsync(RefreshRequest refreshRequest, ModelStateDictionary modelState) {
			if (!modelState.IsValid) {
				return BadRequestModelState(modelState);
			}

			bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
			if (!isValidRefreshToken) {
				return new BadRequestObjectResult(new ErrorResponse("Invalid refresh token."));
			}

			RefreshToken refreshTokenDTO = await _refreshTokenRepository.GetByTokenAsync(refreshRequest.RefreshToken);

			if (refreshTokenDTO == null) {
				return new NotFoundObjectResult(new ErrorResponse("Invalid refresh token."));
			}

			await _refreshTokenRepository.DeleteAsync(refreshTokenDTO.Id);

			Account account = await _accountService.GetAsync(refreshTokenDTO.AccountId);
			if (account == null) {
				return new NotFoundObjectResult(new ErrorResponse("Account not found."));
			}

			AuthenticatedAccountResponse response = await _authenticator.AuthenticateAsync(account);

			return new OkObjectResult(response);
		}

		public async Task<ActionResult> LogoutAsync(string id) {
			if (!Int32.TryParse(id, out int userId)) {
				return new UnauthorizedResult();
			}
			await _refreshTokenRepository.DeleteAllAsync(userId);

			return new NoContentResult();
		}

		private ActionResult BadRequestModelState(ModelStateDictionary modelState) {
			IEnumerable<string> errorMessages = modelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage));
			return new BadRequestObjectResult(new ErrorResponse(errorMessages));
		}


	}
}
