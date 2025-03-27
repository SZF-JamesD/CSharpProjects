using System.Windows;
using Ex0602.Services;
using Ex0602.ViewModels;

namespace Ex0602.View
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            var employeeService = new EmployeeService(app.DbService);
            DataContext = new LoginViewModel(employeeService);
        }
    }
}


