using System;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	class UpdateTaskCommand : ICommand {
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private readonly UpdateTaskViewModel _upVM;
		private readonly ITaskDataService _taskDataService;

		public UpdateTaskCommand(UpdateTaskViewModel upVM, ITaskDataService taskDataService) {
			_upVM = upVM;
			_taskDataService = taskDataService;
		}

		public bool CanExecute(object parameter) {
			if (_upVM.TaskToUpdate.Title != "" && _upVM.TaskToUpdate.Description != "") {
				return true;
			}
			else return false;
		}

		public async void Execute(object parameter) {
			Task tas = _upVM.TaskToUpdate;
			await _taskDataService.UpdateAsync(tas);
			_upVM.navigator.ParentViewModel.UpdateData();
			_upVM.CloseView();
		}

	}
}
