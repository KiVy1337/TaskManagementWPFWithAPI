using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Models {
	public class Task : DomainObject{
		public string Title { get; set; }
		public string Description { get; set; }
		public int Progress { get; set; }

		public int IssueId { get; set; }
		public Issue Issue { get; set; }

	}
}
