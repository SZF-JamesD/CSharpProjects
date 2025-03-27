using System.Windows;
using MvvmUtilities.Interfaces;

namespace MvvmUtilities
{
    public class DialogService : IDialogService
    {
        public void ShowMessage(string message, string caption = "Info")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public void ShowError(string message, string caption = "Error")
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public bool AskUserConfirmation(string message, string caption = "Confirmation")
        {
            var result = MessageBox.Show(message, caption, MessageBoxButton.YesNo, MessageBoxImage.Question);
            return result == MessageBoxResult.Yes;
        }
    }
}
