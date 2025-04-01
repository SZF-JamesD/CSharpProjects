using System;

namespace Ex0603.Models
{
    public class Invoice
    {
        public string CompanyName { get; } = "WPFBau";
        public Customer Customer { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal TotalPrice { get; private set; }

        public Invoice(Customer customer, Product product, int quantity)
        {
            Customer = customer;
            Product = product;
            Quantity = quantity;
            InvoiceDate = DateTime.Now;
            CalculateTotal();
        }

        public void CalculateTotal()
        {
            TotalPrice = Product.Price * Quantity * 1.2m;
        }

        public override string ToString()
        {
            return $"{CompanyName}\n\nInvoice for {Customer.CustomerName} (Customer No: {Customer.CustomerNumber})\nProduct: {Product.ProductName}\tQuantity: {Quantity}\tUnit Price: €{Product.Price}\n" +
                $"Total Price (incl. VAT): {TotalPrice:C}\nDate: {InvoiceDate:yyyy-MM-dd  HH:mm:ss}\n";
        }

        public static Invoice FromString(string text)
        {
            var lines = text.Split('\n');
            if (lines.Length < 5) return null;

            string customerName = lines[1].Split(new[] { "Invoice for " }, StringSplitOptions.None)[1].Split(new[] { " (" }, StringSplitOptions.None)[0];
            string customerNumber = lines[1].Split(new[] { "Customer No: " }, StringSplitOptions.None)[1].Replace(")", "");
            string productName = lines[2].Split(new[] {"Product: "}, StringSplitOptions.None)[1].Split(new[] { "\t"}, StringSplitOptions.None)[0];
            int quantity = int.Parse(lines[2].Split(new[] { "Quantity: " }, StringSplitOptions.None)[1].Split(new[] { "\t" }, StringSplitOptions.None)[0]);
            decimal price = decimal.Parse(lines[2].Split(new[] { "Unit Price: " }, StringSplitOptions.None)[1].Replace("€", "").Trim());
            DateTime date = DateTime.Parse(lines[4].Split(new[] { "Date: " }, StringSplitOptions.None)[1]);

            var customer = Customer.GetOrCreateCustomer(customerName, customerNumber);
            var product = new Product(productName, price);

            return new Invoice(customer, product, quantity) { InvoiceDate = date };
        }
    }
}
