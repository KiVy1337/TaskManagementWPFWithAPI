using System;
using System.Windows.Input;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	class UpdateStateTaskViewModelCommand : ICommand {
		public event EventHandler CanExecuteChanged {
			add { CommandManager.RequerySuggested += value; }
			remove { CommandManager.RequerySuggested -= value; }
		}

		private readonly TasksViewModel _tasksVM;

		public UpdateStateTaskViewModelCommand(TasksViewModel tasksVM) {
			_tasksVM = tasksVM;
		}

		public bool CanExecute(object parameter) {
			return true;
		}

		public  void Execute(object parameter) {
			_tasksVM.InnerTasksNavigator.CurrentViewModel = null;
			_tasksVM.SelectedIssue = null;
			_tasksVM.SelectedItem = null;
		}
	}
}
