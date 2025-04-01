using Ex0603.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Ex0603.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void InvoiceListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (InvoiceListView.SelectedItem is Invoice selectedInvoice)
            {
                InvoiceView detailWindow = new InvoiceView(selectedInvoice);
                detailWindow.Show();
            }
        }
    }
}
