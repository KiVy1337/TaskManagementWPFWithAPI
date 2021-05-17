using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models {
	public class Account : DomainObject {
		public string Email { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public DateTime DatesJoined { get; set; }
		public ICollection<Issue> Issues { get; set; }
	}
}
