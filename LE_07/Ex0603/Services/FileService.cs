using System;
using System.IO;
using System.Threading.Tasks;
using MvvmUtilities.Interfaces;

namespace Ex0603.Services
{
    internal class FileService
    {
        private readonly IDialogService _dialogService;

        public FileService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public async Task SaveInvoiceAsync(string companyName, string customerName, string customerNumber,
            string productName, int quantity, decimal pricePerUnit, decimal totalPrice)
        {
            string fileName = $"Invoice_{DateTime.Now:yyyyMMddHHmmss}.txt";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

            string content = $"Company Name: {companyName}\nCustomer Name: {customerName}\nCustomer Number: {customerNumber}\nProduct Name: {productName}\nQuantity: {quantity}\n" +
                $"Price per Unit: {pricePerUnit}\nTotal Price (incl. 20% VAT): {totalPrice:C}\n";

            await Task.Run(() => File.WriteAllText(filePath, content));
            _dialogService.ShowMessage($"Invoice saved successfully.\n{filePath}", "Invoice Saved");
        }
    }
}
