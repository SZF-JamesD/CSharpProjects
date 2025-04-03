using Ex0701.Models;
using Ex0701.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex0701.Views
{
    internal class Menu
    {
        private FilterService filterService;
        private Dictionary<string, Action> menuActions;
        private bool exit = false;

        public Menu(FilterService filterService)
        {
            this.filterService = filterService;

            menuActions = new Dictionary<string, Action>
            {
                { "1", FilterByCity},
                { "2", () => DisplayFilteredResults(filterService.FilterCustomers(c => c.Age < 30, AskQuerySyntax())) },
                { "3", () => DisplayFilteredResults(filterService.FilterCustomers(c => c.OrderValue > 100, AskQuerySyntax())) },
                { "4", () => DisplayFilteredResults(filterService.FilterCustomers(c => c.ProductCategory == "Electronics", AskQuerySyntax())) },
                { "5", () => DisplayFilteredResults(filterService.FilterCustomers(c => c.OrderDate > new DateTime(2023, 1, 1), AskQuerySyntax())) },
                { "6", () => DisplayFilteredResults(filterService.FilterAndSortCustomers(c => true, AskQuerySyntax(), c => c.Name, false))},
                { "7", GroupBycity},
                { "8", () => DisplayFilteredResults(filterService.FilterAndSortCustomers(c => true, AskQuerySyntax(), c => c.Age, true).Take(3)) },
                { "9", Exit }
            };
        }


        public void DisplayMenu()
        {
            while (!exit)
            {
                try
                {
                    Console.WriteLine("\n-- Customer Filter System --");
                    Console.WriteLine("1. Filter by City\n2. Show Customers under 30\n3. Show orders over €100\n4. Show Electronics Orders\n5. Show orders after 1.1.2023\n6. Sort Customers by Name\n7. Group Customers by City\n8. Show oldest Customers\n9. Exit");
                    Console.Write("Please select an option: ");

                    string choice = Console.ReadLine()?.Trim();
                    if (menuActions.TryGetValue(choice, out Action action))
                    {
                        action.Invoke();
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please try again.");
                    }
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Invalid input. Please try again.");
                }
            }
        }



       
        private void DisplayFilteredResults(IEnumerable<Customer> customers)
        {
            foreach (var customer in customers)
            {
                Console.WriteLine(customer.ToString());
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }

        private void FilterByCity()
        {
            while (true)
            {
                Console.WriteLine("Enter city name: ");
                string city = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(city))
                {
                    Console.WriteLine("Invalid input. Input cannot be empty.");
                }
                else
                {
                    DisplayFilteredResults(filterService.FilterCustomers(c => c.City.Equals(city, StringComparison.OrdinalIgnoreCase), AskQuerySyntax()));
                    break;
                }
            }
        }

        private void GroupBycity()
        {
            var groupedByCity = filterService.FilterCustomers(c => true, AskQuerySyntax())
                .GroupBy(c => c.City)
                .Select(group => new
                {
                    City = group.Key,
                    Customers = group.ToList()
                });

            foreach (var cityGroup in groupedByCity)
            {
                Console.WriteLine($"Customers in {cityGroup}: ");
                foreach (var customer in cityGroup.Customers)
                {
                    Console.WriteLine(customer.ToString());
                }
            }
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }


        private bool AskQuerySyntax()
        {
            while (true)
            {
                Console.Write("Use Query Syntax? (y/n): ");
                string input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y") return true;
                if (input == "n") return false;

                Console.WriteLine("Invalid input. Please enter y or n.");
            }
        }


        private void Exit()
        {
            Console.WriteLine("Exiting program.");
            exit = true;
        }
    }
}
