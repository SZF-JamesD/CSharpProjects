/*using MvvmUtilities;
using Ex0603.Services;
using Ex0603.Views;
using Ex0603.ViewModels;
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

            var loginViewModel = new LoginViewModel(authenticationService, dialogService);

            var loginView = new LoginView
            {
                DataContext = loginViewModel 
            };

            loginView.Show();
        }
    }
}*/
using Ex0603.Services;
using Ex0603.ViewModels;
using Ex0603.Views;
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


