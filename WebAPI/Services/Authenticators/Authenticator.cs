using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Models.Responses;
using WebAPI.Services.RefreshTokenRepositories;
using WebAPI.Services.TokenGenerators;

namespace WebAPI.Services.Authenticators {
	public class Authenticator {
		private readonly AccessTokenGenerator _accessTokenGenerator;
		private readonly RefreshTokenGenerator _refreshTokenGenerator;
		private readonly IRefreshTokenRepository _refreshTokenRepository;

		public Authenticator(AccessTokenGenerator accessTokenGenerator, RefreshTokenGenerator refreshTokenGenerator,
			IRefreshTokenRepository refreshTokenRepository) {

			_accessTokenGenerator = accessTokenGenerator;
			_refreshTokenGenerator = refreshTokenGenerator;
			_refreshTokenRepository = refreshTokenRepository;
		}

		public async Task<AuthenticatedAccountResponse> AuthenticateAsync(Account account) {
			string accessToken = _accessTokenGenerator.GenerateToken(account);
			string refreshToken = _refreshTokenGenerator.GenerateToken();

			RefreshToken refreshTokenDTO = new RefreshToken() {
				Token = refreshToken,
				AccountId = account.Id
			};
			await _refreshTokenRepository.CreateAsync(refreshTokenDTO);

			return new AuthenticatedAccountResponse(accessToken, refreshToken);

		}
	}
}
