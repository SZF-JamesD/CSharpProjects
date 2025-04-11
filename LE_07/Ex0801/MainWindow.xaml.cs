using Ex0801.ViewModels;
using System.Windows;

namespace Ex0801
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;           
            
        }
    }
}
