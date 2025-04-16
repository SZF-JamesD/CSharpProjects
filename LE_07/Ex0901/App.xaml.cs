using Ex0901.Interfaces;
using Ex0901.Services;
using Ex0901.ViewModels;
using Ex0901.Views;
using Microsoft.Extensions.DependencyInjection;
using MvvmUtilities;
using MvvmUtilities.Interfaces;
using System;
using System.Windows;

namespace Ex0901
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindowViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();

            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();

            base.OnStartup(e);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<IJsonService, JsonService>();
            services.AddSingleton<ILocationService, LocationService>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MapViewModel>();
            services.AddTransient<SettingsViewModel>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<MainView>();
            services.AddTransient<MapView>();
            services.AddTransient<SettingsView>();
        }
    }
}
