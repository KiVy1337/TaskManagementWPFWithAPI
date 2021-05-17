using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.WPF.Commands;

namespace TaskManagement.WPF.ViewModels {
	public class IssuesTasksViewModel : ViewModelBase {
		public IssuesViewModel IssuesViewModel { get; set; }
		public TasksViewModel TasksViewModel { get; set; }

		public IssuesTasksViewModel(CreateViewModel<IssuesViewModel> createIssuesViewModel, CreateViewModel<TasksViewModel> createTasksViewModel) {
			TasksViewModel = createTasksViewModel();
			IssuesViewModel = createIssuesViewModel();
			IssuesViewModel.UpdateStateTaskViewModelCommand = new UpdateStateTaskViewModelCommand(TasksViewModel);
		}
	}
}
