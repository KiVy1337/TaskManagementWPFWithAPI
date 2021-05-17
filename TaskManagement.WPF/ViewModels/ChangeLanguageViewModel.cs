using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TaskManagement.WPF.Commands;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;

namespace TaskManagement.WPF.ViewModels {
	public class ChangeLanguageViewModel : ViewModelBase {
		private CultureInfo language;
		private bool _isVisibilityNeeded;

		public bool IsVisibilityNeeded {
			get {
				return _isVisibilityNeeded;
			}
			set {
				_isVisibilityNeeded = value;
				OnPropertyChanged(nameof(IsVisibilityNeeded));
			}
		}

		public CultureInfo Language {
			get {
				return language;
			}
			set {
				language = value;
				OnPropertyChanged(nameof(Language));
			}
		}
		public List<CultureInfo> Languages { get; set; }
		
		public IAuthenticator Authenticator { get; set; }
		public ICommand ChangeLanguageCommand { get; }
		public ICommand ViewLoginCommand { get; }

		public ChangeLanguageViewModel(IAuthenticator authenticator, IRenavigator loginRenavigator) {
			App.LanguageChanged += LanguageChanged;
			ViewLoginCommand = new RenavigateCommand(loginRenavigator);
			Authenticator = authenticator;
			if (authenticator.IsLoggedIn) {
				IsVisibilityNeeded = false;
			}
			else {
				IsVisibilityNeeded = true;
			}
			ChangeLanguageCommand = new ChangeLanguageCommand(this);
			Languages = App.Languages;
			Language = App.Language;
		}

		private void LanguageChanged(Object sender, EventArgs e) {
			Language = App.Language;
		}
	}
}
