using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Services {
	public interface IDataService<T> {
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<T> CreateAsync(T entity);
		Task<int> UpdateAsync(T entity);
		Task<bool> DeleteRangeAsync(IEnumerable<T> entities);
		Task<bool> IsExistsAsync(int id);
	}
}
