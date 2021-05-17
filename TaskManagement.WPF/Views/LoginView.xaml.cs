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

namespace TaskManagement.WPF.Views {
	/// <summary>
	/// Логика взаимодействия для LoginView.xaml
	/// </summary>
	public partial class LoginView : UserControl {

		public static readonly DependencyProperty LoginCommandProperty =
			DependencyProperty.Register("LoginCommand", typeof(ICommand), typeof(LoginView), new PropertyMetadata(null));
		public static readonly DependencyProperty GotPasswordProperty =
			DependencyProperty.Register("GotPassword", typeof(bool), typeof(LoginView), new PropertyMetadata(null));

		public ICommand LoginCommand {
			get { return (ICommand)GetValue(LoginCommandProperty); }
			set { SetValue(LoginCommandProperty, value); }
		}
		public bool GotPassword {
			get { return (bool)GetValue(GotPasswordProperty); }
			set { SetValue(GotPasswordProperty, value); }
		}
		public LoginView() {
			InitializeComponent();
		}

		private void Login_Click(object sender, RoutedEventArgs e) {
	
			if (LoginCommand != null) {
				string password = pbPassword.Password;
				LoginCommand.Execute(password);
			}
		}

		private void pbPassword_PasswordChanged(object sender, RoutedEventArgs e) {
			if (pbPassword.Password != "") {
				GotPassword = true;
			}
			else {
				GotPassword = false;
			}
		}
	}
}
