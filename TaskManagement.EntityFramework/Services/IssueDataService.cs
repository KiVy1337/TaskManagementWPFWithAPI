using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;
using System.Data.Entity;
using TaskManagement.Domain.Services;
using TaskManagement.EntityFramework.Services.Common;

namespace TaskManagement.EntityFramework.Services {
	//Service that has NonQueryDataService<Account> and also can load issues' tasks
	public class IssueDataService : IDataService<Issue> {
		private readonly NonQueryDataService<Issue> _nonQueryDataService;

		public IssueDataService() {
			_nonQueryDataService = new NonQueryDataService<Issue>();
		}

		public async Task<Issue> Create(Issue entity) {
			return await _nonQueryDataService.Create(entity);
		}

		public async Task<bool> DeleteRange(IEnumerable<Issue> entities) {
			return await _nonQueryDataService.DeleteRange(entities);
		}

		public async Task<Issue> Get(int id) {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				Issue entity = await context.Issues.Include(i => i.Tasks).FirstOrDefaultAsync((e) => e.Id == id);

				return entity;
			}
		}

		public async Task<IEnumerable<Issue>> GetAll() {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				IEnumerable<Issue> entities = await context.Issues.Include(a => a.Tasks).ToListAsync();

				return entities;
			}
		}

		public async Task<int> Update(Issue entity) {
			return await _nonQueryDataService.Update(entity);
		}
	}
}
