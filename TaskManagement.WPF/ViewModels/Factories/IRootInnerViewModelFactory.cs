using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels.Factories {
	public interface IRootInnerViewModelFactory<T> where T : DomainObject {
		InnerViewModel CreateViewModel(ViewType viewType);
		IInnerNavigator<T> Navigator { get; set; }
	}
}
