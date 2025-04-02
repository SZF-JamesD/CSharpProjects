using Ex0603.Models;
using Ex0603.ViewModels;
using System.Windows;

namespace Ex0603.Views
{
    public partial class InvoiceView : Window
    {
        public InvoiceView(Invoice invoice)
        {
            InitializeComponent();
            DataContext = new InvoiceViewModel(invoice);
        }
    }
}
