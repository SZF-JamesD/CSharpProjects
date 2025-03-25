using System.Windows;
using Ex0602.Services;

namespace Ex0602.UI
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string employeeId = EmployeeIdTextBox.Text.Trim();

            if (string.IsNullOrEmpty(employeeId))
            {
                MessageBox.Show("Please enter an Employee ID.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            bool isValid = EmployeeService.ValidateEmployeeId(employeeId);

            if (isValid)
            {
                MainWindow mainWindow = new MainWindow();
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