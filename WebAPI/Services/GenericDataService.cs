using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.Services.Common;

namespace WebAPI.Services {
	public class GenericDataService<T> : IDataService<T> where T : DomainObject {
		private readonly NonQueryDataService<T> _nonQueryDataService;
		private readonly TaskManagementDBContext _context;

		public GenericDataService(TaskManagementDBContext context) {
			_context = context;
			_nonQueryDataService = new NonQueryDataService<T>(context);
		}

		public async Task<T> CreateAsync(T entity) {
			return await _nonQueryDataService.CreateAsync(entity);
		}

		public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities) {
			return await _nonQueryDataService.DeleteRangeAsync(entities);
		}

		public async Task<T> GetAsync(int id) {
			T entity = await _context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);

			return entity;
		}

		public async Task<IEnumerable<T>> GetAllAsync() {
			IEnumerable<T> entities = await _context.Set<T>().ToListAsync();

			return entities;
		}

		public async Task<int> UpdateAsync(T entity) {
			return await _nonQueryDataService.UpdateAsync(entity);
		}

		public async Task<bool> IsExistsAsync(int id) {
			return await _nonQueryDataService.IsExistsAsync(id);
		}
	}
}
