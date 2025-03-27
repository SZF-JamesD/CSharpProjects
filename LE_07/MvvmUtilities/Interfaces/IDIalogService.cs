namespace MvvmUtilities.Interfaces
{
    public interface IDialogService
    {
        void ShowMessage(string message, string caption = "Info");
        void ShowError(string message, string caption = "Error");
        bool AskUserConfirmation(string message, string caption = "Confirmation");
    }
}
