using System;
using System.Windows.Input;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.Commands {
	// Command that closes inner window in application
	class CloseInnerViewCommand : ICommand {
		public event EventHandler CanExecuteChanged;
		private readonly InnerViewModel _innerVM;

		public CloseInnerViewCommand(InnerViewModel innerViewModel) {
			_innerVM = innerViewModel;
		}

		public bool CanExecute(object parameter) {
			return true;
		}

		public void Execute(object parameter) {
			_innerVM.CloseView();
		}
	}
}
