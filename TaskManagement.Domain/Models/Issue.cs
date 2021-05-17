using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.Models {
	public class Issue : DomainObject{
		public string Title { get; set; }
		public DateTime StartDate { get; set; }
		public string Status { get; set; }
		public ICollection<Task> Tasks { get; set; }

		public int AccountId { get; set; }
		public Account Account { get; set; }


	}
}
