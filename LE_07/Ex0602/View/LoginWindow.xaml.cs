using System.Windows;
using Ex0602.Services;

namespace Ex0602.UI
{
    public partial class LoginWindow : Window
    {
    private readonly EmployeeService employeeService;

        public LoginWindow()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            employeeService = new EmployeeService(app.DbService);
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string employeeIdText = EmployeeIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(employeeIdText))
            {
                MessageBox.Show("Please enter an Employee ID.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(employeeIdText, out int id))
            {
                MessageBox.Show("Employee ID must be a number.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isValid = await employeeService.ValidateEmployeeIdAsync(id);

            if (isValid)
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Employee ID. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}


