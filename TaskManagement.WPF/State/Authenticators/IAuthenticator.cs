using System.Threading.Tasks;
using TaskManagement.Domain.Models;

public enum RegistrationResult {
	Success,
	EmailAlreadyExists,
	UsernameAlreadyExists,
	InvalidEmail
}

namespace TaskManagement.WPF.State.Authenticators {
	public interface IAuthenticator {
		Account CurrentAccount { get; }
		bool IsLoggedIn { get; }
		Task<bool> TryToAuthenticateAsync();
		Task<bool> RefreshAccessToken();
		Task<RegistrationResult> Register(string email, string username, string password, string confirmPassword);
		System.Threading.Tasks.Task Login(string username, string password);
		System.Threading.Tasks.Task LogoutAsync();
		string GetAccess();
	}
}
