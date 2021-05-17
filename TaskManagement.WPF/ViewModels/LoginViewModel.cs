using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels {
	public class LoginViewModel : ViewModelBase {

		private string _username;
		private bool _gotPassword;
		public string Username {
			get {
				return _username;
			}
			set {
				_username = value;
				OnPropertyChanged(nameof(Username));
				OnPropertyChanged(nameof(IsLoginEnabled));
			}
		}

		public bool GotPassword {
			get {
				return _gotPassword;
			}
			set {
				_gotPassword = value;
				OnPropertyChanged(nameof(IsLoginEnabled));
			}
		}

		public bool IsLoginEnabled {
			get {
				if (GotPassword && (Username != "")) {
					return true;
				}
				else {
					return false;
				}
			}
		}
		public MessageViewModel ErrorMessageViewModel { get; }

		public string ErrorMessage {
			set => ErrorMessageViewModel.Message = value;
		}
		public ICommand LoginCommand { get; }
		public ICommand ViewRegisterCommand { get; }

		public LoginViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator, IRenavigator registerRenavigator) {
			ErrorMessageViewModel = new MessageViewModel();
			LoginCommand = new LoginCommand(this, authenticator, loginRenavigator);

			ViewRegisterCommand = new RenavigateCommand(registerRenavigator);
		}
	}
}
