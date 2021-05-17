using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TaskManagement.Domain.Exceptions;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	// command for login in application
	class LoginCommand : ICommand {
		private readonly LoginViewModel _loginViewModel;
		private readonly IAuthenticator _authenticator;
		private readonly IRenavigator _renavigator;

		public LoginCommand(LoginViewModel loginViewModel, IAuthenticator authenticator, IRenavigator renavigator) {
			_authenticator = authenticator;
			_loginViewModel = loginViewModel;
			_renavigator = renavigator;
		}

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public bool CanExecute(object parameter) {
			if (parameter.ToString() != string.Empty && _loginViewModel.Username != string.Empty) {
				return true;
			}
			else {
				return false;
			}
		}

		public async void Execute(object parameter) {
			ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
										  where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Lang.")
										  select d).First();
			try {
				await _authenticator.Login(_loginViewModel.Username, parameter.ToString());
				_renavigator.Renavigate();
			}
			catch (UserNotFoundException) {
				_loginViewModel.ErrorMessage = oldDict["Errors_UsernameDoesntExists"].ToString();
			}
			catch (InvalidPasswordException) {
				_loginViewModel.ErrorMessage = oldDict["Errors_IncorrectPassword"].ToString();
			}
			catch (Exception) {
				_loginViewModel.ErrorMessage = oldDict["Errors_Failed"].ToString();
			}
		}
	}
}
