using Ex0801.Services;
using MvvmUtilities;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Ex0801.Interfaces;
using MvvmUtilities.Interfaces;

namespace Ex0801.ViewModels
{
    public class AddUserViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService;

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                SetProperty(ref _userName, value);
                ((AsyncRelayCommand)AddUserCommand).RaiseCanExecuteChanged();
            }
            }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                ((AsyncRelayCommand)AddUserCommand).RaiseCanExecuteChanged();
            }
            }


        public ICommand AddUserCommand { get; set; }

        public AddUserViewModel(IDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            _dialogService = dialogService;
            AddUserCommand = new AsyncRelayCommand(async () => await AddUserAsync(), 
                canExecute: () => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password));
        }

        private async Task AddUserAsync()
        {
            try
            {
                await _dataService.AddNewUserAsync(UserName, Password);
                _dialogService.ShowMessage("User created successfully!");
            }
            catch (InvalidOperationException ex)
            {
                _dialogService.ShowError("Invalid input: " + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                _dialogService.ShowError("Error: " + ex.Message);
            }
        }
    }
}
