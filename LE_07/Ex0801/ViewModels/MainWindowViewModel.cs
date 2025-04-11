using Ex0801.Interfaces;
using Ex0801.Services;
using Ex0801.Views;
using Microsoft.Extensions.DependencyInjection;
using MvvmUtilities;
using MvvmUtilities.Interfaces;

namespace Ex0801.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService;
        private object _currentView;

        public object CurrentView
        {
            get => _currentView; 
            set => SetProperty(ref _currentView, value);
        }

        public int LoggedEmployeeId { get; set; }


        public MainWindowViewModel(IDialogService dialogService, IDataService dataServíce)
        {
            _dialogService = dialogService;
            _dataService = dataServíce;

            var loginVM = App.ServiceProvider.GetRequiredService<LoginViewModel>();
            loginVM.SetLoginSuccessCallback(OnLoginSuccess);

            var loginView = App.ServiceProvider.GetRequiredService<LoginView>();
            loginView.DataContext = loginVM;
            CurrentView = loginView;
        }

        private void OnLoginSuccess()
        {
            var mainVM = App.ServiceProvider.GetRequiredService<MainViewModel>();
            mainVM.LoggedEmployeeId = this.LoggedEmployeeId;

            var mainView = App.ServiceProvider.GetRequiredService<MainView>();
            mainView.DataContext = mainVM;
            CurrentView = mainView;
        }
    }
}
