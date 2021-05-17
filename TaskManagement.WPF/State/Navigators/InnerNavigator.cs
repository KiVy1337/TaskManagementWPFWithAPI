using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.Models;
using TaskManagement.WPF.ViewModels;
using TaskManagement.WPF.ViewModels.Factories;

namespace TaskManagement.WPF.State.Navigators {
	// navigator that can change inner VM in IssuesTasksVM
	public class InnerNavigator<T> : ObservableObject, IInnerNavigator<T> where T: DomainObject{
		private InnerViewModel _currentViewModel;
		private GenericViewModel<T> _parentViewModel;
		
		public InnerViewModel CurrentViewModel {
			get {
				return _currentViewModel;
			}
			set {
				_currentViewModel = value;
				OnPropertyChanged(nameof(CurrentViewModel));

			}
		}

		public GenericViewModel<T> ParentViewModel {
			get {
				return _parentViewModel;
			}
			set {
				_parentViewModel = value;
				OnPropertyChanged(nameof(ParentViewModel));
			}
		}


		public ICommand UpdateCurrentViewModelCommand { get; set; }

		public InnerNavigator(IRootInnerViewModelFactory<T> viewModelFactory) {
			UpdateCurrentViewModelCommand = new UpdateInnerViewModelCommand<T>(this, viewModelFactory);
		}
	}
}
