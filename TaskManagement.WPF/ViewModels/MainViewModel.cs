using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;
using TaskManagement.WPF.ViewModels.Factories;

namespace TaskManagement.WPF.ViewModels {
	public class MainViewModel : ViewModelBase {
		public INavigator<ViewModelBase> Navigator { get; set; }
		public IAuthenticator Authenticator { get; }
		public ICommand UpdateCurrentViewModelCommand { get; }
		public ICommand LogoutCommand { get; }

		public string Username {
			get {
				if(Authenticator.CurrentAccount == null) {
					return null;
				}
				else {
					return Authenticator.CurrentAccount.Username;
				}
			}
		}


		public  MainViewModel(INavigator<ViewModelBase> navigator, IRootTaskManagerViewModelFactory taskManagerViewModelFactory, IAuthenticator authenticator) {

			Navigator = navigator;
			Authenticator = authenticator;
			UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, taskManagerViewModelFactory);
			LogoutCommand = new LogoutCommand(this);
			System.Threading.Tasks.Task<bool> t = System.Threading.Tasks.Task.Run(() => Authenticator.TryToAuthenticateAsync());
			t.Wait();
			bool isAuthenticated =  t.Result;
			if (isAuthenticated) {
				UpdateCurrentViewModelCommand.Execute(ViewType.IssuesTasks);
			}
			else {
				UpdateCurrentViewModelCommand.Execute(ViewType.Login);
			}
		}
	}
}
