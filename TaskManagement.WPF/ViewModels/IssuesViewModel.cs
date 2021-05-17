using System.Collections.Generic;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels {
	public class IssuesViewModel : GenericViewModel<Issue> {
		private readonly IAuthenticator _authenticator;
		private readonly IAccountDataService _accountDataService;

		private ICollection<Issue> _issues;
		private Issue _selectedItem;
		private string _buttonVisibility;

		public IInnerNavigator<Issue> InnerIssuesNavigator { get; set; }

		public ICommand DeleteIssuesCommand { get; set; }
		public ICommand UpdateStateTaskViewModelCommand { get; set; }

		public ICollection<Issue> Issues {
			get {
				return _issues;
			}
			set {
				_issues = value; 
				OnPropertyChanged(nameof(Issues));
			}
		}

		public TasksViewModel TasksVM { get; set; }

		public override Issue SelectedItem {
			get {
				return _selectedItem;
			}
			set {
				_selectedItem = value;
				if (value != null) {
					ButtonVisibility = "Visible";
				}
				else {
					ButtonVisibility = "Collapsed";
				}
				OnPropertyChanged(nameof(SelectedItem));
			}
		}
		public string ButtonVisibility {
			get {
				return _buttonVisibility;
			}
			set {
				_buttonVisibility = value;
				OnPropertyChanged(nameof(ButtonVisibility));
			}
		}

		public IssuesViewModel(IInnerNavigator<Issue> nav, IAuthenticator authenticator, IAccountDataService accountDataService, IIssueDataService issueDataService) {
			SelectedItem = null;
			InnerIssuesNavigator = nav;
			_authenticator = authenticator;
			_accountDataService = accountDataService;
			InnerIssuesNavigator.ParentViewModel = this;
			DeleteIssuesCommand = new DeleteIssuesCommand(this, issueDataService);
			if (authenticator.IsLoggedIn) {
				LoadIssues();
			}
		}
		public void LoadIssues() {
			System.Threading.Tasks.Task<Account> t = System.Threading.Tasks.Task.Run(() => _accountDataService.GetAsync());
			t.Wait();
			Account account = t.Result;
			if (t.Result != null) {
				Issues = account.Issues;
			}
		}

		public override async void UpdateData() {
			Account account = await _accountDataService.GetAsync();
			if (account != null) {
				Issues = account.Issues;
			}
			UpdateStateTaskViewModelCommand.Execute(null);
		}

	}
}
