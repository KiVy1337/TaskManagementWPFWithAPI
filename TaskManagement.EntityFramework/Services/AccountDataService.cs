using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;
using TaskManagement.Domain.Services;
using TaskManagement.EntityFramework.Services.Common;

namespace TaskManagement.EntityFramework.Services {

	//Service that has NonQueryDataService<Account> and also can load accounts issues
	public class AccountDataService : IAccountService {
		private readonly NonQueryDataService<Account> _nonQueryDataService;

		public AccountDataService() {
			_nonQueryDataService = new NonQueryDataService<Account>();
		}

		public async Task<Account> Create(Account entity) {
			return await _nonQueryDataService.Create(entity);
		}

		public async Task<bool> DeleteRange(IEnumerable<Account> entities) {
			return await _nonQueryDataService.DeleteRange(entities);
		}

		public async Task<Account> Get(int id) {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				Account entity = await context.Accounts.Include(a => a.Issues).FirstOrDefaultAsync((e) => e.Id == id);

				return entity;
			}
		}

		public async Task<IEnumerable<Account>> GetAll() {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				IEnumerable<Account> entities = await context.Accounts.Include(a => a.Issues).ToListAsync();

				return entities;
			}
		}

		public async Task<Account> GetByEmail(string email) {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				return await context.Accounts
					.Include(a => a.Issues)
					.FirstOrDefaultAsync(a => a.Email == email);
			}
		}

		public async Task<Account> GetByUsername(string username) {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				return await context.Accounts
					.Include(a => a.Issues)
					.FirstOrDefaultAsync(a => a.Username == username);
			}
		}

		public async Task<int> Update(Account entity) {
			return await _nonQueryDataService.Update(entity);
		}
	}
}
