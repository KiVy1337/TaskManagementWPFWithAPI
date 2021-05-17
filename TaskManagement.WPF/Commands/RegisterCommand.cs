using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	public class RegisterCommand : ICommand {
		private readonly RegisterViewModel _registerViewModel;
		private readonly IAuthenticator _authenticator;
		private readonly IRenavigator _registerRenavigator;

		public RegisterCommand(RegisterViewModel registerViewModel,
			IAuthenticator authenticator,
			IRenavigator registerRenavigator) {

			_registerViewModel = registerViewModel;
			_authenticator = authenticator;
			_registerRenavigator = registerRenavigator;
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter) {
			if (parameter.ToString() != string.Empty && _registerViewModel.Email != string.Empty && _registerViewModel.Username != string.Empty) {
				return true;
			}
			else {
				return false;
			}
		}

		public async void Execute(object parameter) {
			_registerViewModel.ErrorMessage = string.Empty;
			ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
										  where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Lang.")
										  select d).First();
			try {
				string password = parameter.ToString();
				RegistrationResult registrationResult = await _authenticator.Register(
					_registerViewModel.Email,
					_registerViewModel.Username,
					password,
					password
					);
				switch (registrationResult) {
					case RegistrationResult.Success:
						_registerRenavigator.Renavigate();
						break;
					case RegistrationResult.EmailAlreadyExists:
						_registerViewModel.ErrorMessage = oldDict["Errors_EmailAccountAlreadyExists"].ToString();
						break;
					case RegistrationResult.UsernameAlreadyExists:
						_registerViewModel.ErrorMessage = oldDict["Errors_UsernameAccountAlreadyExists"].ToString();
						break;
					case RegistrationResult.InvalidEmail:
						_registerViewModel.ErrorMessage = oldDict["Errors_InvalidEmail"].ToString();
						break;
					default:
						_registerViewModel.ErrorMessage = oldDict["Errors_Failed"].ToString();
						break;
				}
			}
			catch (Exception) {
				_registerViewModel.ErrorMessage = oldDict["Errors_Failed"].ToString();
			}
		}
	}
}
