using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;
using TaskManagement.Domain.Services;
using TaskManagement.EntityFramework;
using TaskManagement.EntityFramework.Services;

namespace Test {
	class Program {
		static void Main(string[] args) {
			IDataService<User> userService = new GenericDataService<User>(new TaskManagementDbContext());
			Console.WriteLine(userService.Create(new User { Username = "KiVy"}).Result);
			Console.ReadLine();
		}
	}
}

