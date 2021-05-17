using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.WPF.State.Authenticators.Responses;

namespace TaskManagement.WPF.Services.SaveTokens {
	public interface ITokenProviderService {
		string Path { get; set; }
		void WriteTokensToFile(AuthenticatedAccountResponse tokens);
		AuthenticatedAccountResponse GetTokensFromFile();
		void DeleteFile();
	}
}
