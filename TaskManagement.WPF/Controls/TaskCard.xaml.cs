using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TaskManagement.WPF.Controls {
	/// <summary>
	/// Логика взаимодействия для TaskCard.xaml
	/// </summary>
	public partial class TaskCard : UserControl {
		public static readonly DependencyProperty TitleProperty;
		public static readonly DependencyProperty DescriptionProperty;
		public static readonly DependencyProperty ProgressProperty;

		public string Title {
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}
		public string Description {
			get { return (string)GetValue(DescriptionProperty); }
			set { SetValue(DescriptionProperty, value); }
		}
		public int Progress {
			get { return (int)GetValue(ProgressProperty); }
			set { SetValue(ProgressProperty, value); }
		}


		static TaskCard() {
			TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(TaskCard));
			DescriptionProperty = DependencyProperty.Register("Description", typeof(string), typeof(TaskCard));
			ProgressProperty = DependencyProperty.Register("Progress", typeof(int), typeof(TaskCard));
		}

		public TaskCard() {
			InitializeComponent();
		}
	}
}
