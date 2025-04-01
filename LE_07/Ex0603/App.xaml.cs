using Ex0603.Services;
using Ex0603.ViewModels;
using MvvmUtilities;
using System.Windows;

namespace Ex0603
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var authenticationService = new AuthenticationService();
            var dialogService = new DialogService();

            var mainViewModel = new MainViewModel(authenticationService, dialogService);

            var mainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };

            mainWindow.Show();
        }
    }
}


