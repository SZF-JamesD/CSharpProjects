using System;
using Ex0301.Services;
using System.Collections.Generic;

namespace Ex0301.UI
{
    public class Menu
    {
        private AddressBookService addressBookService;
        private Dictionary<string, Action> menuActions;
        private InputHandler inputHandler;
        private bool exit = false;

        public Menu()
        {
            addressBookService = new AddressBookService();
            inputHandler = new InputHandler(addressBookService);
            menuActions = new Dictionary<string, Action>
            {
                { "1", inputHandler.HandleAddAddress },
                { "2", inputHandler.HandleRemoveAddress },
                { "3", inputHandler.HandleFindAddress },
                { "4", addressBookService.DisplayAllAddresses },
                { "5", Exit }
            };
        }

        public void DisplayMenu()
        {
            while (!exit)
            {
                try
                {
                    Console.WriteLine("\n-- Address Book --");
                    Console.WriteLine("1. Add Address");
                    Console.WriteLine("2. Remove Address");
                    Console.WriteLine("3. Find Address");
                    Console.WriteLine("4. Display All Addresses");
                    Console.WriteLine("5. Exit");
                    Console.Write("Please select an option: ");

                    string choice = Console.ReadLine()?.Trim();

                    if (menuActions.TryGetValue(choice, out Action action))
                    {
                        action.Invoke();
                    }
                    else
                    {
                        Console.WriteLine("Invalid Option. Please try again.");
                    }
                }
                catch (NullReferenceException)
                {
                    Console.WriteLine("Invalid Option. Please try again.");
                }
            }
        }

        public void Exit()
        {
            Console.WriteLine("Exiting program.");
            exit = true;
        }
    }
}

