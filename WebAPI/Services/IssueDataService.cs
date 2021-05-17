using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services.Common;

namespace WebAPI.Services {
	public class IssueDataService : IDataService<Issue> {
		private readonly NonQueryDataService<Issue> _nonQueryDataService;
		private readonly TaskManagementDBContext _context;


		public IssueDataService(TaskManagementDBContext context) {
			_nonQueryDataService = new NonQueryDataService<Issue>(context);
			_context = context;
		}

		public async Task<Issue> CreateAsync(Issue entity) {
			return await _nonQueryDataService.CreateAsync(entity);
		}

		public async Task<bool> DeleteRangeAsync(IEnumerable<Issue> entities) {
			return await _nonQueryDataService.DeleteRangeAsync(entities);
		}

		public async Task<Issue> GetAsync(int id) {
			Issue entity = await _context.Issues.Include(i => i.Tasks).FirstOrDefaultAsync((e) => e.Id == id);

			return entity;
		}

		public async Task<IEnumerable<Issue>> GetAllAsync() {
			IEnumerable<Issue> entities = await _context.Issues.Include(a => a.Tasks).ToListAsync();

			return entities;
		}

		public async Task<int> UpdateAsync(Issue entity) {
			return await _nonQueryDataService.UpdateAsync(entity);
		}
		public async Task<bool> IsExistsAsync(int id) {
			return await _nonQueryDataService.IsExistsAsync(id);
		}
	}
}
