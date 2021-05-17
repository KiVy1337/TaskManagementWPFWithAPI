using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services.Common;

namespace WebAPI.Services {
	public class AccountDataService : IAccountService {
		private readonly NonQueryDataService<Account> _nonQueryDataService;
		private readonly TaskManagementDBContext _context;

		public AccountDataService(TaskManagementDBContext context) {
			_context = context;
			_nonQueryDataService = new NonQueryDataService<Account>(context);
		}

		public async Task<Account> CreateAsync(Account entity) {
			return await _nonQueryDataService.CreateAsync(entity);
		}

		public async Task<bool> DeleteRangeAsync(IEnumerable<Account> entities) {
			return await _nonQueryDataService.DeleteRangeAsync(entities);
		}

		public async Task<Account> GetAsync(int id) {
			Account entity = await _context.Accounts.Include(a => a.Issues).FirstOrDefaultAsync((e) => e.Id == id);

			return entity;
		}

		public async Task<IEnumerable<Account>> GetAllAsync() {
			IEnumerable<Account> entities = await _context.Accounts.Include(a => a.Issues).ToListAsync();

			return entities;
		}

		public async Task<Account> GetByEmailAsync(string email) {
			return await _context.Accounts
				.Include(a => a.Issues)
				.FirstOrDefaultAsync(a => a.Email == email);

		}

		public async Task<Account> GetByUsernameAsync(string username) {
			return await _context.Accounts
				.Include(a => a.Issues)
				.FirstOrDefaultAsync(a => a.Username == username);
		}

		public async Task<int> UpdateAsync(Account entity) {
			return await _nonQueryDataService.UpdateAsync(entity);
		}

		public async Task<bool> IsExistsAsync(int id) {
			return await _nonQueryDataService.IsExistsAsync(id);
		}
	}
}
