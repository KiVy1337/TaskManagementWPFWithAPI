using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;

namespace TaskManagement.WPF.Services.DataServices {
	public interface IAccountDataService {
		Task<Account> GetAsync();
	}
}
