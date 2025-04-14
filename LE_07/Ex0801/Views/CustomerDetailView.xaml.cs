using Ex0801.ViewModels;
using System.Windows;

namespace Ex0801.Views
{
    public partial class CustomerDetailView : Window
    {
        public CustomerDetailView(CustomerDetailViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
