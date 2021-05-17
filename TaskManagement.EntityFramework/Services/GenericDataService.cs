using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using TaskManagement.Domain.Services;
using TaskManagement.Domain.Models;
using TaskManagement.EntityFramework.Services.Common;

namespace TaskManagement.EntityFramework.Services {
	//CRUD-interface
	public class GenericDataService<T> : IDataService<T> where T : DomainObject{
		private readonly NonQueryDataService<T> _nonQueryDataService;

		public GenericDataService() {
			_nonQueryDataService = new NonQueryDataService<T>();
		}

		public async Task<T> Create(T entity) {
			return await _nonQueryDataService.Create(entity);
		}

		public async Task<bool> DeleteRange(IEnumerable<T> entities) {
			return await _nonQueryDataService.DeleteRange(entities);
		}

		public async Task<T> Get(int id) {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);

				return entity;
			}
		}

		public async Task<IEnumerable<T>> GetAll() {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				IEnumerable<T> entities = await context.Set<T>().ToListAsync();

				return entities;
			}
		}

		public async Task<int> Update(T entity) {
			return await _nonQueryDataService.Update(entity);
		}
	}
}
