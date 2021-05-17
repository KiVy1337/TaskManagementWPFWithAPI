using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.WPF.State.Authenticators.Responses;

namespace TaskManagement.WPF.Services.SaveTokens {
	public class TokenProviderService : ITokenProviderService {
		public string Path { get; set; } = "tokens.txt";

		public AuthenticatedAccountResponse GetTokensFromFile() {
			if (File.Exists(Path)) {
				// deserialize JSON directly from a file
				using (StreamReader file = File.OpenText(Path)) {
					JsonSerializer serializer = new JsonSerializer();
					return (AuthenticatedAccountResponse)serializer.Deserialize(file, typeof(AuthenticatedAccountResponse));
				}
			}
			else {
				return null;
			}
		}

		public void WriteTokensToFile(AuthenticatedAccountResponse tokens) {

			using (StreamWriter file = File.CreateText(Path)) {
				JsonSerializer serializer = new JsonSerializer();
				//serialize object directly into file stream
				serializer.Serialize(file, tokens);
			}
		}

		public void DeleteFile() {
			File.Delete(Path);
		}

	}
}
