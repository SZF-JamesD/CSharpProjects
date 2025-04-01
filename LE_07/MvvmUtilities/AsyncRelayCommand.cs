using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmUtilities
{
    public class AsyncRelayCommand<T> : ICommand
    {
        private readonly DialogService _dialogService;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private bool _isExecuting;

        public event EventHandler CanExecuteChanged;

        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute = null, DialogService dialogService = null)
        {
            _execute = execute ?? throw new ArgumentException(nameof(execute));
            _canExecute = canExecute;
            _dialogService = dialogService;
        }

        
        public bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute == null || _canExecute((T)parameter));
        }

        public async void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            _isExecuting = true;
            RaiseCanExecuteChanged();

            try
            {
                await _execute((T)parameter);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AsyncRelayCommand: {ex.Message}");
                _dialogService.ShowError(ex.Message, "An error occured");
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
