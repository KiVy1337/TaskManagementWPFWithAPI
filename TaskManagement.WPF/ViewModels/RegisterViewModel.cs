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
	public class RegisterViewModel : ViewModelBase {
		private string _email;
		private bool _gotPassword;
		private bool _gotConfirmPassword;
		public string Email {
			get {
				return _email;
			}
			set {
				_email = value;
				OnPropertyChanged(nameof(Email));
				OnPropertyChanged(nameof(IsRegisterEnabled));
			}
		}

		private string _username;
		public string Username {
			get {
				return _username;
			}
			set {
				_username = value;
				OnPropertyChanged(nameof(Username));
				OnPropertyChanged(nameof(IsRegisterEnabled));
			}
		}
		public bool GotPassword {
			get {
				return _gotPassword;
			}
			set {
				_gotPassword = value;
				OnPropertyChanged(nameof(IsRegisterEnabled));
			}
		}
		public bool GotConfirmPassword {
			get {
				return _gotConfirmPassword;
			}
			set {
				_gotConfirmPassword = value;
				OnPropertyChanged(nameof(IsRegisterEnabled));
			}
		}

		public bool IsRegisterEnabled {
			get {
				if (GotPassword && GotConfirmPassword && (Username != "") && (Email != "")) {
					return true;
				}
				else {
					return false;
				}
			}
		}

		public ICommand RegisterCommand { get; }

		public ICommand ViewLoginCommand { get; }

		public MessageViewModel ErrorMessageViewModel { get; }

		public string ErrorMessage {
			set => ErrorMessageViewModel.Message = value;
		}


		public RegisterViewModel(IAuthenticator authenticator, IRenavigator registerRenavigator, IRenavigator loginRenavigator) {
			ErrorMessageViewModel = new MessageViewModel();
			RegisterCommand = new RegisterCommand(this, authenticator, registerRenavigator);
			ViewLoginCommand = new RenavigateCommand(loginRenavigator);
		}

	}
}

