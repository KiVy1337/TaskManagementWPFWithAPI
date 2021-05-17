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
	/// Логика взаимодействия для IssueCard.xaml
	/// </summary>
	public partial class IssueCard : UserControl {
		public static readonly DependencyProperty TitleProperty;
		public static readonly DependencyProperty StartDateProperty;
		public static readonly DependencyProperty StatusProperty;

		public string Title {
			get { return (string)GetValue(TitleProperty); }
			set { SetValue(TitleProperty, value); }
		}
		public DateTime? StartDate {
			get { return (DateTime?)GetValue(StartDateProperty); }
			set { SetValue(StartDateProperty, value); }
		}
		public string Status {
			get { return (string)GetValue(StatusProperty); }
			set { SetValue(StatusProperty, value); }
		}

		static IssueCard() {
			TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(IssueCard));
			StartDateProperty = DependencyProperty.Register("StartDate", typeof(DateTime?), typeof(IssueCard));
			StatusProperty = DependencyProperty.Register("Status", typeof(string), typeof(IssueCard));
		}



		public IssueCard() {
			InitializeComponent();
		}
	}
}
