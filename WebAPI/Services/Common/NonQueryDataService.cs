using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services.Common {
	public class NonQueryDataService<T> where T : DomainObject {

		private readonly TaskManagementDBContext _context;

		public NonQueryDataService(TaskManagementDBContext context) {
			_context = context;
		}
		public async Task<T> CreateAsync(T entity) {
			_context.Set<T>().Add(entity);
			await _context.SaveChangesAsync();

			return entity;
		}

		public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities) {
			DbSet<T> items = _context.Set<T>();
			for (int i = 0; i < entities.Count(); i++) {
				items.Attach(entities.ElementAt(i));
			}
			items.RemoveRange(entities);
			await _context.SaveChangesAsync();

			return true;

		}
		public async Task<int> UpdateAsync(T entity) {
			_context.Entry(entity).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return entity.Id;
			
		}

		public async Task<bool> IsExistsAsync(int id) {
			return await _context.Set<T>().AnyAsync(x => x.Id == id);
		}
	}
}
