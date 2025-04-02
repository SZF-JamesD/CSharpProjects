using Ex0603.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Ex0603.Services
{
    internal class InvoiceService
    {
        private readonly string _folderPath;

        public InvoiceService(string folderPath)
        {
            _folderPath = folderPath;
            if (!Directory.Exists(_folderPath))
                Directory.CreateDirectory(_folderPath);
        }


        public List<Invoice> LoadInvoices()
        {
            var invoices = new List<Invoice>();
            var files = Directory.GetFiles(_folderPath, "*.txt");

            foreach (var file in files)
            {
                string text = File.ReadAllText(file);
                var invoice = Invoice.FromString(text);
                if (invoice != null)
                {
                    invoices.Add(invoice);
                }
            }
            return invoices;
        }

        public async Task SaveInvoiceAsync(Invoice invoice)
        {
            string fileName = $"Invoice_{invoice.Customer.CustomerNumber}.txt";
            string filePath = Path.Combine(_folderPath, fileName);


            await Task.Run(() => File.WriteAllText(filePath, invoice.ToString()));
        }
    }
}
