using Ex0801.Services;
using Ex0801.Views;
using Microsoft.Extensions.DependencyInjection;
using MvvmUtilities;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Ex0801.Interfaces;
using MvvmUtilities.Interfaces;

namespace Ex0801.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IDataService _dataService;
        private Action _onLoginSuccess;

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand ToAddNewUserCommand { get; }

        public LoginViewModel(IDialogService dialogService, IDataService dataService)
        {
            _dialogService = dialogService;
            _dataService = dataService;

            LoginCommand = new AsyncRelayCommand(LoginAsync);
            ToAddNewUserCommand = new RelayCommand(AddUser);
        }

        public void SetLoginSuccessCallback(Action callback) => _onLoginSuccess = callback;

        private async Task LoginAsync()
        {
            try
            {
                if (await _dataService.UserExistsAsync(Username, Password))
                {
                    _onLoginSuccess?.Invoke();
                }
                else
                {
                    _dialogService.ShowError("Invalid Credentials", "Login Error");
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowError("Error: " +ex, "Login Error");
            }
        }


        private void AddUser()
        {
            var viewModel = App.ServiceProvider.GetRequiredService<AddUserViewModel>();
            AddUserView addUserview = new AddUserView(viewModel);
            addUserview.Show();
        }
    }
}
