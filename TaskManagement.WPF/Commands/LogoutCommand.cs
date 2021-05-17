using System;
using System.Windows.Input;
using TaskManagement.WPF.State.Navigators;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	// command for logout
	class LogoutCommand : ICommand {
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private readonly MainViewModel _mainVM;


		public LogoutCommand(MainViewModel mainVM) {
			_mainVM = mainVM;
		}

		public bool CanExecute(object parameter) {
			if (_mainVM.Authenticator.IsLoggedIn == false){
				return false;
			}
			else {
				return true;
			}
		}

		public async void Execute(object parameter) {
			await _mainVM.Authenticator.LogoutAsync();
			_mainVM.UpdateCurrentViewModelCommand.Execute(ViewType.Login);

		}
	}
}
