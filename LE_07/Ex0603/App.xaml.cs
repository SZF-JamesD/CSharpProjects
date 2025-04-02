using Ex0603.Views;
using System.Windows;

namespace Ex0603
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var loginWindow = new LoginView();

            loginWindow.Show();
        }
    }
}


