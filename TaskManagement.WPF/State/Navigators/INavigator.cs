using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.WPF.ViewModels;

namespace TaskManagement.WPF.State.Navigators {
	//enum with types of window
	public enum ViewType {
		IssuesTasks,
		ChangeLanguage,
		Login,
		IssuesAdd,
		IssuesUpdate,
		TasksAdd,
		TasksUpdate
	}
	public interface INavigator<T>{
		T CurrentViewModel { get; set; }

	}
}
