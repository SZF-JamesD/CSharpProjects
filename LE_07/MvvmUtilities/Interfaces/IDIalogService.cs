namespace MvvmUtilities.Interfaces
{
    public interface IDIalogService
    {
        void ShowMessage(string message, string caption);
        void ShowError(string message, string caption);
        bool ShowConfirmation(string message, string caption);
    }
}
