using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.WPF.ViewModels;
using TaskManagement.WPF.ViewModels.Factories;

namespace TaskManagement.WPF.State.Navigators {
	// this class change state of application by changing current VM
	public class ViewModelDelegateRenavigator<TViewModel> : IRenavigator where TViewModel : ViewModelBase {

		private readonly INavigator<ViewModelBase> _navigator;
		private readonly CreateViewModel<TViewModel> _createViewModel;

		public ViewModelDelegateRenavigator(INavigator<ViewModelBase> navigator, CreateViewModel<TViewModel> createViewModel) {
			_navigator = navigator;
			_createViewModel = createViewModel;
		}

		public void Renavigate() {
			_navigator.CurrentViewModel = _createViewModel(); 
		}
	}
}
