using Ex0801.ViewModels;
using System.Windows;

namespace Ex0801.Views
{
    public partial class AddCustomerView : Window
    {
        public AddCustomerView(AddCustomerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
