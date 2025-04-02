using Ex0603.Services;
using Ex0603.ViewModels;
using System.Windows;
using MvvmUtilities;

namespace Ex0603
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var dialogService = new DialogService();
            var authService = new AuthenticationService();

            DataContext = new MainViewModel(authService, dialogService);
        }
    }
}
