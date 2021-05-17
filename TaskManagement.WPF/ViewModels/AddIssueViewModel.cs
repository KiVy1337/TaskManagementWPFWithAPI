using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels {
	class AddIssueViewModel : InnerViewModel {
		private readonly IAuthenticator _authenticator;
		private IEnumerable<string> _statusList;
		public IInnerNavigator<Issue> navigator;
		private Issue _issueToAdd;

		public ICommand AddIssueCommand { get; set; }
		public ICommand CloseInnerViewCommand { get; set; }

		public IEnumerable<string> StatusList {
			get {
				return _statusList;
			}
			set {
				_statusList = value;
				OnPropertyChanged(nameof(StatusList));
			}
		}
		public Issue IssueToAdd {
			get {
				return _issueToAdd;
			}
			set {
				_issueToAdd = value;
				OnPropertyChanged(nameof(IssueToAdd));
			}
		}
		public AddIssueViewModel(INavigator<InnerViewModel> nnavigator, IAuthenticator authenticator, IIssueDataService issueDataService) {
			navigator = (IInnerNavigator<Issue>)nnavigator;
			_authenticator = authenticator;

			StatusList = new List<string>() { "New", "In progress" };

			IssueToAdd = new Issue {
				AccountId = _authenticator.CurrentAccount.Id,
				Title = "",
				StartDate = DateTime.Today,
				Status = ""
			};
			AddIssueCommand = new AddIssueCommand(this, issueDataService);
			CloseInnerViewCommand = new CloseInnerViewCommand(this);
		}

		public override void CloseView() {
			navigator.CurrentViewModel = null;
		}
	}
}
