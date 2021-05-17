using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Models;

namespace TaskManagement.WPF.ViewModels {
	public class GenericViewModel<T> : ViewModelBase where T : DomainObject {
		public virtual T SelectedItem { get; set; }
		public virtual void UpdateData() {

		}

		public virtual Issue SelectedIssue { get; set; }
		
	}
}
