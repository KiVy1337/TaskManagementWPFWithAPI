using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Models;

namespace TaskManagement.WPF.ViewModels {

	public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

	public class ViewModelBase : ObservableObject {

	}
}
