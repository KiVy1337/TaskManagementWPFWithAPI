using System;
using System.Windows.Input;
using TaskManagement.WPF.State.Navigators;
using TaskManagement.WPF.ViewModels;
using TaskManagement.WPF.ViewModels.Factories;

namespace TaskManagement.WPF.Commands {
	// command that sets new VM in Navigator 
	public class UpdateCurrentViewModelCommand : ICommand {
		public event EventHandler CanExecuteChanged;

		private readonly INavigator<ViewModelBase> _navigator;
		private readonly IRootTaskManagerViewModelFactory _taskManagerViewModelAbstractFactory;

		public UpdateCurrentViewModelCommand(INavigator<ViewModelBase> navigator, IRootTaskManagerViewModelFactory taskManagerViewModelAbstractFactory) {
			_navigator = navigator;
			_taskManagerViewModelAbstractFactory = taskManagerViewModelAbstractFactory;
		}

		public bool CanExecute(object parameter) {
			return true;
		}

		public void Execute(object parameter) {
			if (parameter is ViewType) {
				ViewType viewType = (ViewType) parameter;
				_navigator.CurrentViewModel = _taskManagerViewModelAbstractFactory.CreateViewModel(viewType);
			}
		}
	}
}