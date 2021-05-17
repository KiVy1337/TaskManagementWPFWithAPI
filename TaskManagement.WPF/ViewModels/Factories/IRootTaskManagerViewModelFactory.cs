using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels.Factories {
	public interface IRootTaskManagerViewModelFactory {
		ViewModelBase CreateViewModel(ViewType viewType);
	}
}
