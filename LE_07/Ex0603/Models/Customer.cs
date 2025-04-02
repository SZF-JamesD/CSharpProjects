using System.Collections.Generic;

namespace Ex0603.Models
{
    public class Customer
    {
        public string CustomerName { get; set; }
        public string CustomerNumber { get; }

        private static readonly Dictionary<string, Customer> _customerRegistry = new Dictionary<string, Customer>();
        private static int _customerCounter = 0;

        public Customer(string customerName, string customerNumber)
        {
            CustomerName = customerName;
            CustomerNumber = customerNumber;
        }

        public static Customer GetOrCreateCustomer(string customerName = null, string customerNumber = null)
        {
            if (string.IsNullOrWhiteSpace(customerName) && !string.IsNullOrWhiteSpace(customerNumber))
            {
                foreach (var customer in _customerRegistry.Values)
                {
                    if (customer.CustomerNumber == customerNumber)
                    {
                        return customer;
                    }
                }
                return null;
            }
            if (_customerRegistry.TryGetValue(customerName, out Customer existingCustomer))
            {
                return existingCustomer;
            }
            else
            {
                if (string.IsNullOrEmpty(customerNumber))
                {
                    _customerCounter++;
                    customerNumber = $"KU-{_customerCounter:D4}";
                }
                else
                {
                    string numPart = customerNumber.Replace("KU-", "");
                    if (int.TryParse(numPart, out int parsedNum))
                    {
                        if (parsedNum > _customerCounter)
                            _customerCounter = parsedNum;
                    }
                }
                var newCustomer = new Customer(customerName, customerNumber);
                _customerRegistry[customerName] = newCustomer;
                return newCustomer;
            }
        }
    }
}
