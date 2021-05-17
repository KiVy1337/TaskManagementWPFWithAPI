using System;
using System.Windows.Input;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	// Command that add new Task in DB
	class AddTaskCommand : ICommand {
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private readonly AddTaskViewModel _addVM;
		private readonly ITaskDataService _taskDataService;

		public AddTaskCommand(AddTaskViewModel addVM, ITaskDataService taskDataService) {
			_addVM = addVM;
			_taskDataService = taskDataService;
		}

		public bool CanExecute(object parameter) {
			if (_addVM.TaskToAdd.Title != "" && _addVM.TaskToAdd.Description != "") {
				return true;
			}
			else return false;
		}

		public async void Execute(object parameter) {
			await _taskDataService.CreateAsync(_addVM.TaskToAdd);
			_addVM.navigator.ParentViewModel.UpdateData();
			_addVM.CloseView();
		}
	}
}
