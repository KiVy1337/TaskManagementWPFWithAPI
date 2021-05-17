using System;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.State.Navigators;
using TaskManagement.WPF.ViewModels.Factories;

namespace TaskManagement.WPF.Commands {
	// command that sets new Vm in InnerNavigator
	class UpdateInnerViewModelCommand<T> : ICommand where T : DomainObject {
		public event EventHandler CanExecuteChanged;

		private readonly IInnerNavigator<T> _navigator;
		private readonly IRootInnerViewModelFactory<T> _rootFactory;

		public UpdateInnerViewModelCommand(IInnerNavigator<T> navigator, IRootInnerViewModelFactory<T> taskManagerViewModelAbstractFactory) {
			_navigator = navigator;
			_rootFactory = taskManagerViewModelAbstractFactory;
		}

		public bool CanExecute(object parameter) {
			return true;
		}

		public void Execute(object parameter) {
			if (parameter is ViewType) {
				ViewType viewType = (ViewType)parameter;
				_rootFactory.Navigator = _navigator;
				_navigator.CurrentViewModel = _rootFactory.CreateViewModel(viewType);
			}
		}
	}
}
