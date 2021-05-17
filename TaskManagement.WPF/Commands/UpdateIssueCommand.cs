using System;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	class UpdateIssueCommand : ICommand {
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private readonly UpdateIssueViewModel _upVM;
		private readonly IIssueDataService _issueDataService;

		public UpdateIssueCommand(UpdateIssueViewModel upVM, IIssueDataService issueDataService) {
			_upVM = upVM;
			_issueDataService = issueDataService;
		}

		public bool CanExecute(object parameter) {
			if (_upVM.IssueToUpdate.Title != "" && _upVM.IssueToUpdate.Status != "") {
				return true;
			}
			else return false;
		}

		public async void Execute(object parameter) {
			Issue iss = _upVM.IssueToUpdate;
			await _issueDataService.UpdateAsync(iss);
			_upVM.CloseView();
			_upVM._navigator.ParentViewModel.UpdateData();
		}

	}
}
