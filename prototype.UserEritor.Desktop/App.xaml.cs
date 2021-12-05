using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using prototype.UserEritor.Desktop.Service;
using prototype.UserEritor.Desktop.Views;

namespace prototype.UserEritor.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider;
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(ServiceCollection services)
        {
           
            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IUserSettingsService, UserSettingsService>();
            services.AddSingleton<IThemeService, ThemeService>();

            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainViewModel>();
            services.AddScoped<UserListViewModel>();
            services.AddScoped<ThemeSelectorViewModel>();
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow =  ServiceProvider.GetService<MainWindow>();
            var mainViewModel = ServiceProvider.GetService<MainViewModel>();
     
            mainWindow.DataContext = mainViewModel;
            mainWindow.ShowDialog();
        }
    }
}
