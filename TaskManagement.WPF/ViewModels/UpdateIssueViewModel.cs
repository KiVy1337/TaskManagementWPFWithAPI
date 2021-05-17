using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels {
	class UpdateIssueViewModel : InnerViewModel {
		private IEnumerable<string> _statusList;
		private Issue _issueToUpdate;
		private Issue _originIssue;

		public IInnerNavigator<Issue> _navigator;

		public ICommand UpdateIssueCommand { get; set; }
		public ICommand CloseInnerViewCommand { get; set; }

		public Issue IssueToUpdate {
			get {
				return _issueToUpdate;
			}
			set {
				_issueToUpdate = value;
				OnPropertyChanged(nameof(IssueToUpdate));
			}
		}
		public Issue OriginIssue {
			get {
				return _originIssue;
			}
			set {
				_originIssue = value;
				OnPropertyChanged(nameof(OriginIssue));
			}
		}

		public IEnumerable<string> StatusList {
			get {
				return _statusList;
			}
			set {
				_statusList = value;
				OnPropertyChanged(nameof(StatusList));
			}
		}

		public UpdateIssueViewModel(INavigator<InnerViewModel> navigator, IIssueDataService issueDataService) {
			_navigator = (IInnerNavigator<Issue>)navigator;
			StatusList = new List<string>() { "New", "In progress", "Closed" };
			OriginIssue = _navigator.ParentViewModel.SelectedItem;
			IssueToUpdate = new Issue() {
				Id = OriginIssue.Id,
				AccountId = OriginIssue.AccountId,
				Account = OriginIssue.Account,
				Title = OriginIssue.Title,
				Status = OriginIssue.Status,
				StartDate = OriginIssue.StartDate
			};
			UpdateIssueCommand = new UpdateIssueCommand(this, issueDataService);
			CloseInnerViewCommand = new CloseInnerViewCommand(this);
		}
		public override void CloseView() {
			_navigator.CurrentViewModel = null;
		}
	}
}
