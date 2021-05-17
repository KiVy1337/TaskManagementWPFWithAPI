using System;
using System.Windows.Input;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	// Command that addы new Issue in DB
	class AddIssueCommand : ICommand {
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private readonly AddIssueViewModel _addVM;
		private readonly IIssueDataService _issueDataService;

		public AddIssueCommand(AddIssueViewModel addVM, IIssueDataService issueDataService) {
			_addVM = addVM;
			_issueDataService = issueDataService;
		}

		public bool CanExecute(object parameter) {
			if (_addVM.IssueToAdd.Title != "" && _addVM.IssueToAdd.Status != "") {
				return true;
			}
			else return false;
		}

		public async void Execute(object parameter) {
			await _issueDataService.CreateAsync(_addVM.IssueToAdd);
			_addVM.CloseView();
			_addVM.navigator.ParentViewModel.UpdateData();
		}
	}
}
