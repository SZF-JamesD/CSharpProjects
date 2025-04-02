using Ex0603.Services;
using Ex0603.Views;
using MvvmUtilities;
using MvvmUtilities.Interfaces;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Ex0603.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private readonly AuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;

        public ICommand LoginCommand { get; }

        public LoginViewModel(AuthenticationService authenticationService, IDialogService dialogService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            LoginCommand = new RelayCommand(ExecuteLogin);
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private void ExecuteLogin()
        {
            if (_authenticationService.ValidateUser(Username, Password))
            {
                _dialogService.ShowMessage("Login Successful!", "Success");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    var app = (App)Application.Current;
                    mainWindow.Show();

                    var loginWindow = Application.Current.Windows.OfType<LoginView>().FirstOrDefault();
                    loginWindow?.Close();


                });
            }
            else
            {
                _dialogService.ShowError("Invalid user or password!", "Login Failed");
            }
        }
    }
}

