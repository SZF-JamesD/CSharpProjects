using Ex0603.Models;
using MvvmUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex0603.ViewModels
{
    public class InvoiceViewModel : ViewModelBase
    {
        private Invoice _invoice;

        public InvoiceViewModel(Invoice invoice)
        {
            _invoice = invoice;
        }

        public string Title => $"Invoice for Customer: {_invoice.Customer.CustomerNumber} --- {_invoice.InvoiceDate.ToShortDateString()}";

        public string Content => _invoice.ToString();
    }
}
