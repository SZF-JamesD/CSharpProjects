using System;

namespace Ex0701.Models
{
    public class Customer
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string ProductCategory { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderValue { get; set; }

        public override string ToString()
        {
            return $"\nCustomer: {Name}\nAge: {Age}\n{City}\nProduct Catagory: {ProductCategory}\nOrderDate: {OrderDate}\nOrder Value: €{OrderValue}";
        }
    }
}
