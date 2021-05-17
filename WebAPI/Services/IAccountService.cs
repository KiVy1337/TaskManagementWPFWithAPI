using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.Services {
	public interface IAccountService : IDataService<Account> {
		Task<Account> GetByUsernameAsync(string username);
		Task<Account> GetByEmailAsync(string email);

	}
}
