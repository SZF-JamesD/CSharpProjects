using Ex0801.ViewModels;
using System.Windows;

namespace Ex0801.Views
{
    public partial class EditCustomerView : Window
    {
        public EditCustomerView(EditCustomerViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
