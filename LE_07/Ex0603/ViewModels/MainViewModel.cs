/*namespace Ex0603.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand ShowLoginViewCommand { get; }
        public ICommand ShowInvoiceViewCommand { get; }

        public MainViewModel()
        {
            // Initially set CurrentView to LoginViewModel or whatever you prefer
            CurrentView = new LoginViewModel();  // DataContext is set by App.xaml.cs

            // Commands to switch views
            ShowLoginViewCommand = new RelayCommand(() => CurrentView = new LoginViewModel());
            ShowInvoiceViewCommand = new RelayCommand(() => CurrentView = new InvoiceViewModel());
        }
    }
}*/
using Ex0603.Services;
using Ex0603.Views;
using MvvmUtilities;
using MvvmUtilities.Interfaces;
using System.Windows.Input;

namespace Ex0603.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ICommand ShowLoginViewCommand { get; }
        public ICommand ShowInvoiceViewCommand { get; }

        private readonly AuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;

        public MainViewModel(AuthenticationService authenticationService, IDialogService dialogService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;

            CurrentView = new LoginViewModel(_authenticationService, _dialogService, OnLoginSuccess);

            ShowLoginViewCommand = new RelayCommand(() =>
                CurrentView = new LoginViewModel(_authenticationService, _dialogService, OnLoginSuccess));
            ShowInvoiceViewCommand = new RelayCommand(() =>
                CurrentView = new InvoiceViewModel());
        }

        private void OnLoginSuccess()
        {
            CurrentView = new MainView();
        }
    }
}

