using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.RefreshTokenRepositories {
	public class RefreshTokenRepository : IRefreshTokenRepository {
		private readonly TaskManagementDBContext _context;

		public RefreshTokenRepository(TaskManagementDBContext context) {
			_context = context;
		}

		public async  System.Threading.Tasks.Task CreateAsync(RefreshToken refreshToken) {

			_context.RefreshTokens.Add(refreshToken);

			await _context.SaveChangesAsync();
		}

		public async Task<RefreshToken> GetByTokenAsync(string token) {
			return await  _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == token);
		}

		public async System.Threading.Tasks.Task DeleteAsync(int id) {
			RefreshToken refreshToken = await _context.RefreshTokens.FindAsync(id);
			if (refreshToken != null) {
				_context.RefreshTokens.Remove(refreshToken);
				await _context.SaveChangesAsync();
			}
		}

		public async System.Threading.Tasks.Task DeleteAllAsync(int id) {
			IEnumerable<RefreshToken> refreshTokens = await _context.RefreshTokens
				.Where(t => t.AccountId == id)
				.ToListAsync();
			_context.RefreshTokens.RemoveRange(refreshTokens);
			await _context.SaveChangesAsync();
		}
	}
}
