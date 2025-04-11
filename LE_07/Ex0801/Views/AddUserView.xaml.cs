using Ex0801.Services;
using Ex0801.ViewModels;
using MvvmUtilities;
using System.Windows;

namespace Ex0801.Views
{
    public partial class AddUserView : Window
    {
        public AddUserView(AddUserViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
