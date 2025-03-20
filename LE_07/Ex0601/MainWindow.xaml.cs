using Ex0601.Services;
using Ex0601.ViewModels;
using System.Windows;
using System.Windows.Media;
using System;


namespace Ex0601.UI
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeService _employeeService;

        public MainWindow()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainViewModel viewModel)
            {
                try
                {
                    string result = await _employeeService.AddEmployeeAsync(viewModel.Name, viewModel.Age);

                    viewModel.Message = result;

                    viewModel.MessageColor = result.StartsWith("Welcome") ? Brushes.Green : Brushes.Red;
                }
                catch (Exception ex)
                {
                    viewModel.Message = $"An error has occured: {ex.Message}";
                    viewModel.MessageColor = Brushes.Red;
                }
            }
        }
    }
}
