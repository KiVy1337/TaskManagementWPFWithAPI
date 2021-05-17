using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.State.Navigators {
	public interface IInnerNavigator<T> : INavigator<InnerViewModel> where T: DomainObject {
		GenericViewModel<T> ParentViewModel { get; set; }
		ICommand UpdateCurrentViewModelCommand { get; }

	}
}
