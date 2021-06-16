namespace WebAPI.Models {
	public class RefreshToken {
		public int Id { get; set; }
		public string Token { get; set; }
		public int AccountId { get; set; }

	}
}
