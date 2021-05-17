using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.Models;
using TaskManagement.WPF.ViewModels;
using TaskManagement.WPF.ViewModels.Factories;

namespace TaskManagement.WPF.State.Navigators {
	public class Navigator : ObservableObject, INavigator<ViewModelBase>{

		private ViewModelBase _currentViewModel;
		public ViewModelBase CurrentViewModel {
			get {
				return _currentViewModel;
			}
			set {
				_currentViewModel = value;
				OnPropertyChanged(nameof(CurrentViewModel));

			}
		}

	}
}
