using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using TaskAsync = System.Threading.Tasks.Task;

namespace WebAPI.Services.RefreshTokenRepositories {
	public interface IRefreshTokenRepository {
		Task<RefreshToken> GetByToken(string refreshToken);
		TaskAsync Create(RefreshToken refreshToken);
		TaskAsync Delete(int id);
		TaskAsync DeleteAll(int id);
	}
}
