using System.Windows;
using Ex0603.ViewModels;
using Ex0603.Services;
using MvvmUtilities;

namespace Ex0603.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();

            var authService = new AuthenticationService();
            var dialogService = new DialogService();
            this.DataContext = new LoginViewModel(authService, dialogService);
        }

    
    }
}
