using Ex0701.Models;
using System;
using System.Collections.Generic;

namespace Ex0701.Services
{
    internal class CustomerDataService
    {
        private static Random _random = new Random();

        private static List<string> _names = new List<string>
        {
            "Alice", "Bob", "Charlie", "David", "Emma", "Frank", "Grace", "Hannah", "Isaac", "Jack",
            "Liam", "Noah", "Olivia", "Sophia", "Mason", "Logan", "Ethan", "Lucas", "Ava", "Mia",
            "James", "Benjamin", "Elijah", "William", "Alexander", "Michael", "Daniel", "Matthew", "Henry", "Joseph"
        };

        private static List<string> _cities = new List<string>
        {
            "Berlin", "Munich", "Hamburg", "Cologne", "Frankfurt", "Stuttgart", "Düsseldorf", "Dortmund", "Essen", "Leipzig",
            "Bremen", "Dresden", "Hanover", "Nuremberg", "Duisburg", "Bochum", "Wuppertal", "Bielefeld", "Bonn", "Mannheim"
        };

        private static List<string> _productCategories = new List<string>
        {
            "Electronics", "Books", "Clothing", "Sports", "Home Appliances", "Toys", "Furniture", "Groceries", "Automotive", "Beauty",
            "Health", "Music", "Movies", "Gardening", "Office Supplies", "Pet Supplies", "Jewelry", "Shoes", "Bags", "Tools",
            "Watches", "Gaming", "Kitchenware", "Outdoor Equipment", "Fitness Gear", "Travel Accessories", "Baby Products", "Stationery"
        };

        public static List<Customer> GenerateCustomers(int count)
        {
            List<Customer> customers = new List<Customer>();
            for (int i = 0; i < count; i++)
            {
                customers.Add(new Customer
                {
                    Name = _names[_random.Next(_names.Count)],
                    Age = _random.Next(18, 80),
                    City = _cities[_random.Next(_cities.Count)],
                    ProductCategory = _productCategories[_random.Next(_productCategories.Count)],
                    OrderDate = DateTime.Now.AddDays(-_random.Next(1, 1460)),
                    OrderValue = Math.Round((decimal)(_random.NextDouble() * 10000 + 10), 2)
                });
            }
            return customers;
        }
    }
}

