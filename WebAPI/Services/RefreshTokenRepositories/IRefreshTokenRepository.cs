using System.Threading.Tasks;
using WebAPI.Models;
using TaskAsync = System.Threading.Tasks.Task;

namespace WebAPI.Services.RefreshTokenRepositories {
	public interface IRefreshTokenRepository {
		Task<RefreshToken> GetByTokenAsync(string refreshToken);
		TaskAsync CreateAsync(RefreshToken refreshToken);
		TaskAsync DeleteAsync(int id);
		TaskAsync DeleteAllAsync(int id);
	}
}
