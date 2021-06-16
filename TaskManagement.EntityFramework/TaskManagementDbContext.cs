using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManagement.Domain.Models;

namespace TaskManagement.EntityFramework {
	public class TaskManagementDbContext : DbContext {
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Issue> Issues { get; set; }
		public DbSet<Task> Tasks { get; set; }

		// constructor with different ways to initialize data
		public TaskManagementDbContext() : base("DBConnection") {
			Database.SetInitializer<TaskManagementDbContext>(new CreateDatabaseIfNotExists<TaskManagementDbContext>());
		}
		
	}
}
