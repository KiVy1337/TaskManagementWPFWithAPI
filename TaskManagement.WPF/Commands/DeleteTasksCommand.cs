using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	// command that deletes selected tasks
	class DeleteTasksCommand : ICommand {
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private readonly TasksViewModel _isVM;
		private readonly ITaskDataService _taskDataService;

		public DeleteTasksCommand(TasksViewModel isVM, ITaskDataService taskDataService) {
			_isVM = isVM;
			_taskDataService = taskDataService;
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
			IEnumerable<Task> collection = items.Cast<Task>();
			await _taskDataService.DeleteAsync(collection);
			_isVM.InnerTasksNavigator.CurrentViewModel = null;
			_isVM.UpdateData();
		}
	}
}
