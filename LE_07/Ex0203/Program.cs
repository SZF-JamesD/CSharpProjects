using System;
using System.Collections.Generic;
using Ex0203.Services;

namespace Ex0203
{
    internal class Program
    { 
        static void Main()
        {
            PersonService personService = new PersonService();

            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- Person Managment System ---");
                Console.WriteLine("1: Add a Person");
                Console.WriteLine("2. Show Persons by Age");
                Console.WriteLine("3. Show All Persons");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        personService.AddPersonInteractive();
                        break;
                    case "2":
                        personService.ShowPersonsByAgeInteractive();
                        break;
                    case "3":
                        personService.ShowAllPersonsInteractive();
                        break;
                    case "4":
                        Console.WriteLine("Exiting program...");
                            return;
                    default:
                        Console.WriteLine("Invalid choice! Press Enter to try again.");
                        Console.ReadLine();
                        break;
                }
            }
        }
    }
}
