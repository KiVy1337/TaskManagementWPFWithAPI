using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.Services;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels {
	class AddTaskViewModel : InnerViewModel {
		public IInnerNavigator<Task> navigator;
		private Task _taskToAdd;

		public ICommand AddTaskCommand { get; set; }
		public ICommand CloseInnerViewCommand { get; set; }

		public Task TaskToAdd {
			get {
				return _taskToAdd;
			}
			set {
				_taskToAdd = value;
				OnPropertyChanged(nameof(TaskToAdd));
			}
		}
		public AddTaskViewModel(INavigator<InnerViewModel> nnavigator, ITaskDataService taskDataService) {
			navigator = (IInnerNavigator<Task>)nnavigator;

			TaskToAdd = new Task {
				IssueId = navigator.ParentViewModel.SelectedIssue.Id,
				Title = "",
				Description = "",
				Progress = 0
			};
			AddTaskCommand = new AddTaskCommand(this, taskDataService);
			CloseInnerViewCommand = new CloseInnerViewCommand(this);
		}

		public override void CloseView() {
			navigator.CurrentViewModel = null;
		}
	}
}
