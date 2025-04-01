using Ex0603.ViewModels;
using System.Windows;
using System.Windows.Controls;
using Ex0603.ViewModels;

namespace Ex0603.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            DataContext = new LoginViewModel();
        }
    }
}
