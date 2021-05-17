using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;

namespace TaskManagement.WPF.Services.DataServices {
	public interface IIssueDataService {
		Task<Issue> GetAsync(int id);
		Task<Issue> CreateAsync(Issue entity);
		Task<int> UpdateAsync(Issue entity);
		Task<bool> DeleteAsync(IEnumerable<Issue> entities);

	}
}
