using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ex0602.Services;

namespace Ex0602.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private string _employeeId;
        private readonly EmployeeService _employeeService;

        public event PropertyChangedEventHandler PropertyChanged;

        public string EmployeeId
        {
            get => _employeeId;
            set
            {
                if (_employeeId != value)
                {
                    _employeeId = value;
                    OnPropertyChanged(nameof(EmployeeId));

                }
            }
        }

        public ICommand LoginCommand { get; }

        public LoginViewModel(EmployeeService employeeService)
        {
            _employeeService = employeeService;
            LoginCommand = new RelayCommand(async () => await LoginAsync(), CanLogin);
        }

        private bool CanLogin()
        {
            return !string.IsNullOrWhiteSpace(EmployeeId);
        }

        private async Task LoginAsync()
        {
            if (!int.TryParse(EmployeeId, out var id))
            {
                MessageBox.Show("Employee ID must be a number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isValid = await _employeeService.ValidateEmployeeIdAsync(id);

            if (isValid)
            {
                MessageBox.Show("Login successful!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid Employee ID. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public async void Execute(object parameter)
        {
            await _execute();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
