using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	// command that deletes selected issues
	class DeleteIssuesCommand : ICommand {
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private readonly IssuesViewModel _isVM;
		private readonly IIssueDataService _issueDataService;

		public DeleteIssuesCommand(IssuesViewModel isVM, IIssueDataService issueDataService) {
			_isVM = isVM;
			_issueDataService = issueDataService;
		}

		public bool CanExecute(object parameter) {
			if (parameter == null) {
				return false;
			}
			else {
				return true;
			}
		}

		public async void Execute(object parameter) {
			System.Collections.IList items = (System.Collections.IList)parameter;
			IEnumerable<Issue> collection = items.Cast<Issue>();
			await _issueDataService.DeleteAsync(collection);
			_isVM.InnerIssuesNavigator.CurrentViewModel = null;
			_isVM.UpdateData();
		}
	}
}
