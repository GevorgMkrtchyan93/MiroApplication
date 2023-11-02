using Microsoft.Extensions.DependencyInjection;

using Miro.Client.Interfaces;
using Miro.Client.Services;
using Miro.Client.ViewModels;
using Miro.Client.Views;
using Miro.Shared.AuthenticationModels;
using Miro.Shared.Validation;

using System.Windows;

namespace Miro.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceScope ApplicationScope { get; private set; }
       
        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ApplicationScope = serviceCollection.BuildServiceProvider().CreateScope();
            ApplicationScope.ServiceProvider.GetService<ScopeManager>().ApplicationScope = ApplicationScope;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ScopeManager>();
            services.AddScoped<INavigationService, NavigationService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<System.Windows.Input.ICommand, CommandService>();
            //services.AddScoped<IValidation<RegisterModel>, UserValidation>();
            //services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<MainView>();
            services.AddScoped<MainWindow>();
            services.AddScoped<MainWindowViewModel>();
            services.AddScoped<RegisterView>();
            services.AddScoped<RegisterViewModel>();
            services.AddScoped<LoginView>();
            services.AddScoped<LoginViewModel>();
            services.AddScoped<BoardView>();

        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = ApplicationScope.ServiceProvider.GetService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
    public class ScopeManager
    {
        public ScopeManager() { }

        public IServiceScope ApplicationScope { get; set; }
    }

}