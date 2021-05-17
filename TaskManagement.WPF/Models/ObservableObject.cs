using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.WPF.Models {
	// class that implements INotifyPropertyChanged to notify UI about changes
	public class ObservableObject : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		}
	}
}
