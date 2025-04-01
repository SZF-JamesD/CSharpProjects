namespace Ex0603.Models
{
    internal class Customer
    {
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; } //must start with KU-

        public Customer(string customerName, string customerNumber)
        {
            CustomerName = customerName;
            CustomerNumber = customerNumber;
        }
    }
}
