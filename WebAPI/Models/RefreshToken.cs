using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models {
	public class RefreshToken {
		public int Id { get; set; }
		public string Token { get; set; }
		public int AccountId { get; set; }

	}
}
