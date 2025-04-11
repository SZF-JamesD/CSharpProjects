using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MvvmUtilities
{
    public abstract class AsyncCommandBase : ICommand
    {
        protected bool _isExecuting;
        protected readonly DialogService _dialogService;

        public event EventHandler CanExecuteChanged;

        protected AsyncCommandBase(DialogService dialogService = null)
        {
            _dialogService = dialogService;
        }

        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }


    public class AsyncRelayCommand<T> : AsyncCommandBase
    {
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;

        public AsyncRelayCommand(Func<T, Task> execute, Func<T, bool> canExecute = null, DialogService dialogService = null)
            : base(dialogService)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute == null || _canExecute((T)parameter));
        }

        public override async void Execute(object parameter)
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
                Console.WriteLine($"Error in AsyncRelayCommand<T>: {ex.Message}");
                _dialogService?.ShowError(ex.Message, "An error occurred");
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }
    }


    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        public AsyncRelayCommand(Func<Task> execute, Func<bool> canExecute = null, DialogService dialogService = null)
            : base(dialogService)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public override bool CanExecute(object parameter)
        {
            return !_isExecuting && (_canExecute == null || _canExecute());
        }

        public override async void Execute(object parameter)
        {
            if (!CanExecute(parameter)) return;

            _isExecuting = true;
            RaiseCanExecuteChanged();

            try
            {
                await _execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AsyncRelayCommand: {ex.Message}");
                _dialogService?.ShowError(ex.Message, "An error occurred");
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }
    }
}

