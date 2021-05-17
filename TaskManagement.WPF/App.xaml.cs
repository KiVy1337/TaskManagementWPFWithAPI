using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using TaskManagement.Domain.Models;
using TaskManagement.WPF.Services;
using TaskManagement.WPF.Services.DataServices;
using TaskManagement.WPF.Services.SaveTokens;
using TaskManagement.WPF.State.Authenticators;
using TaskManagement.WPF.State.Navigators;
using TaskManagement.WPF.ViewModels;
using TaskManagement.WPF.ViewModels.Factories;

namespace TaskManagement.WPF {
	/// <summary>
	/// Логика взаимодействия для App.xaml
	/// </summary>
	public partial class App : Application {
		private static List<CultureInfo> m_Languages = new List<CultureInfo>();

		//an event for notify the entire application
		public static event EventHandler LanguageChanged;
		//methods and field below supply localization
		public static List<CultureInfo> Languages {
			get {
				return m_Languages;
			}
		}

		public static CultureInfo Language {
			get {
				return System.Threading.Thread.CurrentThread.CurrentUICulture;
			}
			set {
				if (value == null) throw new ArgumentNullException("value");
				if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

				//1. Меняем язык приложения:
				System.Threading.Thread.CurrentThread.CurrentUICulture = value;

				//2. Создаём ResourceDictionary для новой культуры
				ResourceDictionary dict = new ResourceDictionary();
				switch (value.Name) {
					case "ru-RU":
						dict.Source = new Uri(String.Format("Resources/Lang.{0}.xaml", value.Name), UriKind.Relative);
						break;
					default:
						dict.Source = new Uri("Resources/Lang.xaml", UriKind.Relative);
						break;
				}

				//3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
				ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
											  where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Lang.")
											  select d).First();
				if (oldDict != null) {
					int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
					Application.Current.Resources.MergedDictionaries.Remove(oldDict);
					Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
				}
				else {
					Application.Current.Resources.MergedDictionaries.Add(dict);
				}

				//4. Вызываем евент для оповещения всех окон.
				LanguageChanged(Application.Current, new EventArgs());
			}
		}

		protected override  void OnStartup(StartupEventArgs e) {
			App.LanguageChanged += App_LanguageChanged;
			m_Languages.Clear();
			m_Languages.Add(new CultureInfo("en-US")); //Нейтральная культура для этого проекта
			m_Languages.Add(new CultureInfo("ru-RU"));
			Language = TaskManagement.WPF.Properties.Settings.Default.DefaultLanguage;

			IServiceProvider serviceProvider = CreateServiceProvider();


			Window window = serviceProvider.GetRequiredService<MainWindow>();

			window.Show();

			base.OnStartup(e);
		}


		// DI container. Helps to set navigation and infrastructure of application
		private IServiceProvider CreateServiceProvider() {
			IServiceCollection services = new ServiceCollection();

			services.AddSingleton<IAccountDataService, AccountDataService>( s => {
				return new AccountDataService(s.GetRequiredService<IAuthenticator>(), s.GetRequiredService<TaskManagementAccountsHttpClient>(), s.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
			});
			services.AddSingleton<IIssueDataService, IssueDataService>( s => {
				return new IssueDataService(s.GetRequiredService<IAuthenticator>(), s.GetRequiredService<TaskManagementIssuesHttpClient>(), s.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
			});
			services.AddSingleton<ITaskDataService, TaskDataService>(s => {
				return new TaskDataService(s.GetRequiredService<IAuthenticator>(), s.GetRequiredService<TaskManagementTasksHttpClient>(), s.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
			});

			services.AddSingleton<ITokenProviderService, TokenProviderService>();

			services.AddSingleton<IPasswordHasher, PasswordHasher>();

			services.AddSingleton<IRootTaskManagerViewModelFactory, RootTaskManagerViewModelFactory>();
			services.AddSingleton<IRootInnerViewModelFactory<Issue>, RootInnerViewModelFactory<Issue>>();
			services.AddSingleton<IRootInnerViewModelFactory<Task>, RootInnerViewModelFactory<Task>>();

			services.AddScoped<INavigator<ViewModelBase>, Navigator>();
			services.AddScoped<IInnerNavigator<Issue>, InnerNavigator<Issue>>();
			services.AddScoped<IInnerNavigator<Task>, InnerNavigator<Task>>();
			services.AddScoped<IAuthenticator, Authenticator>();
			services.AddScoped<AddIssueViewModel>();
			services.AddScoped<AddTaskViewModel>();
			services.AddScoped<UpdateIssueViewModel>();
			services.AddScoped<UpdateTaskViewModel>();
			services.AddScoped<MainViewModel>();

			//Set http clients
			services.AddHttpClient<TaskManagementAuthenticationHttpClient>(c => {
				c.BaseAddress = new Uri("https://localhost:44332/api/authentication/");
			});
			services.AddHttpClient<TaskManagementAccountsHttpClient>(c => {
				c.BaseAddress = new Uri("https://localhost:44332/api/accounts/");
			});
			services.AddHttpClient<TaskManagementIssuesHttpClient>(c => {
				c.BaseAddress = new Uri("https://localhost:44332/api/issues/");
			});
			services.AddHttpClient<TaskManagementTasksHttpClient>(c => {
				c.BaseAddress = new Uri("https://localhost:44332/api/tasks/");
			});


			services.AddSingleton<CreateViewModel<TasksViewModel>>(s => {
				return () => new TasksViewModel(s.GetRequiredService<IInnerNavigator<Task>>(),
					s.GetRequiredService<IIssueDataService>(),s.GetRequiredService<ITaskDataService>());
			});

			services.AddSingleton<CreateViewModel<ChangeLanguageViewModel>>(s => {
				return () => new ChangeLanguageViewModel(s.GetRequiredService<IAuthenticator>(),
					s.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
			});

			services.AddSingleton<CreateViewModel<IssuesViewModel>>(s => {
				return () => new IssuesViewModel(s.GetRequiredService<IInnerNavigator<Issue>>(),
					s.GetRequiredService<IAuthenticator>(),
					s.GetRequiredService<IAccountDataService>(),
					s.GetRequiredService<IIssueDataService>()
					);
			});

			services.AddSingleton<CreateViewModel<IssuesTasksViewModel>>(s => {
				return () => new IssuesTasksViewModel(s.GetRequiredService<CreateViewModel<IssuesViewModel>>(), s.GetRequiredService<CreateViewModel<TasksViewModel>>());
			});

			services.AddSingleton<ViewModelDelegateRenavigator<LoginViewModel>>();

			services.AddSingleton<CreateViewModel<RegisterViewModel>>(s => {
				return () => new RegisterViewModel(s.GetRequiredService<IAuthenticator>(),
					s.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>(),
					s.GetRequiredService<ViewModelDelegateRenavigator<LoginViewModel>>());
			});

			services.AddSingleton<ViewModelDelegateRenavigator<RegisterViewModel>>();
			services.AddSingleton<ViewModelDelegateRenavigator<IssuesTasksViewModel>>();

			services.AddSingleton<CreateViewModel<LoginViewModel>>(s => {
				return () => new LoginViewModel(s.GetRequiredService<IAuthenticator>(),
					s.GetRequiredService<ViewModelDelegateRenavigator<IssuesTasksViewModel>>(),
					s.GetRequiredService<ViewModelDelegateRenavigator<RegisterViewModel>>()
					);
			});



			services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

			return services.BuildServiceProvider();
		}

		private void App_LanguageChanged(Object sender, EventArgs e) {
			TaskManagement.WPF.Properties.Settings.Default.DefaultLanguage = Language;
			TaskManagement.WPF.Properties.Settings.Default.Save();
		}
	}
}
