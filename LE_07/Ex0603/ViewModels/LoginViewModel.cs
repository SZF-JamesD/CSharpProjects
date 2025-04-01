using Ex0603.Services;
using MvvmUtilities;
using MvvmUtilities.Interfaces;
using System.Windows.Input;

namespace Ex0603.ViewModels
{
    internal class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private readonly AuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly System.Action _loginSuccessCallback; 

        public ICommand LoginCommand { get; }

        public LoginViewModel(AuthenticationService authenticationService, IDialogService dialogService, System.Action loginSuccessCallback)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _loginSuccessCallback = loginSuccessCallback;
            LoginCommand = new RelayCommand<object>(_ => ExecuteLogin());
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

                _loginSuccessCallback?.Invoke();
            }
            else
            {
                _dialogService.ShowError("Invalid user or password!", "Login Failed");
            }
        }
    }
}

