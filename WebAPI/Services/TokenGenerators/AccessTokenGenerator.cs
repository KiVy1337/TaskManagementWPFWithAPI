using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.TokenGenerators {
	public class AccessTokenGenerator {
		private readonly AuthenticationConfiguration _configuration;
		private readonly TokenGenerator _tokenGenerator;

		public AccessTokenGenerator(AuthenticationConfiguration configuration, TokenGenerator tokenGenerator) {
			_configuration = configuration;
			_tokenGenerator = tokenGenerator;
		}

		public string GenerateToken(Account account) {

			List<Claim> claims = new List<Claim>() {
				new Claim("id", account.Id.ToString()),
				new Claim(ClaimTypes.Email, account.Email),
				new Claim(ClaimTypes.Name, account.Username),
			};
			

			return _tokenGenerator.GenerateToken(_configuration.AccessTokenSecret,
				_configuration.Issuer,
				_configuration.Audience,
				_configuration.AccessTokenExpirationMinutes,
				claims);
		}
	}
}
