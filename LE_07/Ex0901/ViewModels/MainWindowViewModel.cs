using MvvmUtilities;
using System.Collections.ObjectModel;

namespace Ex0901.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<object> Views { get; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public MainWindowViewModel(MainViewModel mainVM, SettingsViewModel settingsVM)
        {
            Views = new ObservableCollection<object>
            {
                mainVM,
                settingsVM
            };

            CurrentView = mainVM;
        }
    }
}
