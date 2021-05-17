using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels.Factories {
	// create different view models
	class RootTaskManagerViewModelFactory : IRootTaskManagerViewModelFactory{
		private readonly CreateViewModel<IssuesTasksViewModel> _createIssuesTasksViewModel;
		private readonly CreateViewModel<LoginViewModel> _createLoginViewModel;
		private readonly CreateViewModel<ChangeLanguageViewModel> _createChangeLanguageViewModel;

		public RootTaskManagerViewModelFactory(CreateViewModel<IssuesTasksViewModel> createIssuesTasksViewModel,
			CreateViewModel<LoginViewModel> createLoginViewModel,
			CreateViewModel<ChangeLanguageViewModel> createChangeLanguageViewModel) {

			_createIssuesTasksViewModel = createIssuesTasksViewModel;
			_createLoginViewModel = createLoginViewModel;
			_createChangeLanguageViewModel = createChangeLanguageViewModel;
		}

		public ViewModelBase CreateViewModel(ViewType viewType) {
			switch (viewType) {
				case ViewType.IssuesTasks:
					return _createIssuesTasksViewModel();
				case ViewType.Login:
					return _createLoginViewModel();
				case ViewType.ChangeLanguage:
					return _createChangeLanguageViewModel();
				default:
					throw new ArgumentException("The ViewType doesn't have a ViewModel.", "viewType");
			}
		}
	}
}
