using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models {
	public class TaskManagementDBContext : DbContext {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public TaskManagementDBContext(DbContextOptions<TaskManagementDBContext> options)
            : base(options) {
            Database.EnsureCreated();
        }
    }
}
