using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels {
	class UpdateTaskViewModel : InnerViewModel {
		private Task _taskToUpdate;
		public IInnerNavigator<Task> navigator;

		public ICommand UpdateTaskCommand { get; set; }
		public ICommand CloseInnerViewCommand { get; set; }

		public Task TaskToUpdate {
			get {
				return _taskToUpdate;
			}
			set {
				_taskToUpdate = value;
				OnPropertyChanged(nameof(TaskToUpdate));
			}
		}

		public UpdateTaskViewModel(INavigator<InnerViewModel> nnavigator, ITaskDataService taskDataService) {
			navigator = (IInnerNavigator<Task>)nnavigator;
			Task task = navigator.ParentViewModel.SelectedItem;
			TaskToUpdate = new Task() {
				Id = task.Id,
				IssueId = task.IssueId,
				Title = task.Title,
				Description = task.Description,
				Progress = task.Progress
			};
			UpdateTaskCommand = new UpdateTaskCommand(this, taskDataService);
			CloseInnerViewCommand = new CloseInnerViewCommand(this);
		}
		public override void CloseView() {
			navigator.CurrentViewModel = null;
		}
	}
}
