using Ex0701.Models;
using Ex0701.Services;
using Ex0701.Views;
using System;
using System.Collections.Generic;

namespace Ex0701
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            List<Customer> customers = CustomerDataService.GenerateCustomers(100);
            FilterService filterService = new FilterService(customers);
            Menu menu = new Menu(filterService);
            menu.DisplayMenu();

        }
    }
}
