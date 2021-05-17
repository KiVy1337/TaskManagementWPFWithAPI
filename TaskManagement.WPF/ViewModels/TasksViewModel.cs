using System.Collections.Generic;
using System.Windows.Input;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels {
	public class TasksViewModel : GenericViewModel<Task> {
		private readonly IIssueDataService _issueService;
		public IInnerNavigator<Task> InnerTasksNavigator { get; set; }

		private Issue _selectedIssue;
		private Task _selectedItem;
		private IEnumerable<Task> _tasks;

		private string _updateDeleteButtonsVisibility;
		private string _addButtonVisibility;

		public TasksViewModel(IInnerNavigator<Task> navigator, IIssueDataService issueService, ITaskDataService taskDataService) {
			InnerTasksNavigator = navigator;
			_issueService = issueService;
			InnerTasksNavigator.ParentViewModel = this;
			UpdateDeleteButtonsVisibility = "Collapsed";
			AddButtonVisibility = "Collapsed";
			DeleteTasksCommand = new DeleteTasksCommand(this, taskDataService);
		}
		public ICommand DeleteTasksCommand { get; set; }


		public override Issue SelectedIssue {
			get {
				return _selectedIssue;
			}
			set {
				if (value != null) {
					AddButtonVisibility = "Visible";
				}
				else {
					AddButtonVisibility = "Collapsed";
				}
				_selectedIssue = value;
				OnPropertyChanged(nameof(Tasks));
				OnPropertyChanged(nameof(SelectedIssue));
			}
		}

		public IEnumerable<Task> Tasks {
			get {
				if (SelectedIssue != null) {
					System.Threading.Tasks.Task<Issue> t = System.Threading.Tasks.Task.Run(() => _issueService.GetAsync(_selectedIssue.Id));
					t.Wait();
					_tasks = t.Result.Tasks;
				}
				else {
					_tasks = null;
				}
				SelectedItem = null;
				return _tasks;
			}
		}
		public string UpdateDeleteButtonsVisibility {
			get {
				return _updateDeleteButtonsVisibility;
			}
			set {
				_updateDeleteButtonsVisibility = value;
				OnPropertyChanged(nameof(UpdateDeleteButtonsVisibility));
			}
		}

		public string AddButtonVisibility {
			get {
				return _addButtonVisibility;
			}
			set {
				_addButtonVisibility = value;
				OnPropertyChanged(nameof(AddButtonVisibility));
			}
		}
		public override Task SelectedItem {
			get {
				return _selectedItem;
			}
			set {
				_selectedItem = value;
				if (value != null) {
					UpdateDeleteButtonsVisibility = "Visible";
				}
				else {
					UpdateDeleteButtonsVisibility = "Collapsed";
				}
				OnPropertyChanged(nameof(SelectedItem));
			}
		}

		public override void UpdateData() {
			OnPropertyChanged(nameof(Tasks));
		}
	}
}
