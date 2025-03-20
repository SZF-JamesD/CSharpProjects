using Ex0601.Services;
using Ex0601.ViewModels;
using System.Windows;
using System.Windows.Media;


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

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is MainViewModel viewModel)
            {
                string result = _employeeService.AddEmployee(viewModel.Name, viewModel.Age);

                viewModel.Message = result;

                viewModel.MessageColor = result.StartsWith("Welcome") ? Brushes.Green : Brushes.Red;
            }
        }
    }
}
