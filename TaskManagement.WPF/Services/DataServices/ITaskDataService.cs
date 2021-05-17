using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagement.Domain.Models;

namespace TaskManagement.WPF.Services.DataServices {
	public interface ITaskDataService {
		System.Threading.Tasks.Task<Task> CreateAsync(Task entity);
		System.Threading.Tasks.Task<int> UpdateAsync(Task entity);
		System.Threading.Tasks.Task<bool> DeleteAsync(IEnumerable<Task> entities);
	}
}
