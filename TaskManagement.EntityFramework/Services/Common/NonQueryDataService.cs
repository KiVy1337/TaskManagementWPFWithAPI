using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;

namespace TaskManagement.EntityFramework.Services.Common {
	public class NonQueryDataService<T> where T : DomainObject {
		public async Task<T> Create(T entity) {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				T createdEntity = context.Set<T>().Add(entity);
				await context.SaveChangesAsync();

				return createdEntity;
			}
		}

		public async Task<bool> DeleteRange(IEnumerable<T> entities) {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				DbSet<T> items =  context.Set<T>();
				for (int i = 0; i < entities.Count(); i++) {
					items.Attach(entities.ElementAt(i));
				}
				items.RemoveRange(entities);
				await context.SaveChangesAsync();

				return true;
			}
		}
		public async Task<int> Update(T entity) {
			using (TaskManagementDbContext context = new TaskManagementDbContext()) {
				context.Entry(entity).State = EntityState.Modified;
				await context.SaveChangesAsync();
				return entity.Id;
			}
		}
	}
}
