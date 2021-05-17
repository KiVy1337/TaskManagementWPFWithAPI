using System;
using System.Globalization;
using System.Windows.Input;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	// Command that change language in app
	class ChangeLanguageCommand : ICommand{
		private ChangeLanguageViewModel _changeLanguageVM;

		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		public ChangeLanguageCommand(ChangeLanguageViewModel changeLanguageVM) {
			_changeLanguageVM = changeLanguageVM;
		}

		public bool CanExecute(object parameter) {
			CultureInfo lang = _changeLanguageVM.Language;
			if (lang != null) {
				if (lang != App.Language) {
					return true;
				}
				else {
					return false;
				}
			}
			else {
				return false;
			}
		}

		public void Execute(object parameter) {
			CultureInfo lang = _changeLanguageVM.Language;
			if (lang != null) {
				App.Language = lang;
			}
		}
	}
}
