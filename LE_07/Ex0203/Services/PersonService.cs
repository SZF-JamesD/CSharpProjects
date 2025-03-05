using System;
using System.Collections.Generic;
using System.Linq;
using Ex0203.Models;
using ValidationLib;

namespace Ex0203.Services
{
    public class PersonService
    {
        private List<Person> persons = new List<Person>();
        private const int MaxPersons = 10;
        public int PersonCount => persons.Count;

        //tests
        public bool TryAddPerson(string firstName, string lastName, int age, string phoneNumber)
        {
            if (persons.Count >= MaxPersons)
            {
                // Return false if we have reached the limit
                return false;
            }

            persons.Add(new Person(firstName, lastName, age, phoneNumber));
            return true;
        }

        public List<Person> TryGetPersonsByAge(int maxAge)
        {
            return persons
                    .Where(person => person.Age < maxAge)
                    .OrderBy(person => person.Age)
                    .ToList();
        }


        public List<Person> TryGetAllPersons()
        {
            return new List<Person>(persons);
        }
        //tests end

        public void AddPersonInteractive()
        {
            if (persons.Count >= MaxPersons)
            {
                Console.WriteLine("Cannot add more than 10 persons. Press Enter to continue.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();

            string fullName = firstName + " " + lastName;

            if (ValidationUtil.IsValidFullName(fullName))
                {
                    string formattedName = ValidationUtil.CapitalizeName(fullName);
                    string[] nameParts = formattedName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    firstName = nameParts[0];
                    lastName = nameParts[1];
                }
            else
                { 
                    Console.WriteLine("Invalid name Input");
                    return;
                }

            Console.Write("Enter Age: ");
            if (!int.TryParse(Console.ReadLine(), out int age) || age < 0)
            {
                Console.WriteLine("Invalid age! Press Enter to return.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();
            if (!ValidationUtil.IsValidPhoneNumber(phoneNumber))
            {
                Console.WriteLine("Invalid Phone Number.");
                return; 
            }


            if (TryAddPerson(firstName, lastName, age, phoneNumber))
            {
                Console.WriteLine("Person added successfully! Press Enter to continue.");
            }
            else
            {
                Console.WriteLine("Failed to add person. Press Enter to continue.");
            }
            Console.ReadLine();
        }


        public void ShowPersonsByAgeInteractive()
        {
            Console.Write("Enter the maximum age to filter persons: ");
            if (int.TryParse(Console.ReadLine(), out int maxAge))
            {
                var filteredPersons = TryGetPersonsByAge(maxAge);
                Console.WriteLine($"\n=== Persons Younger Than {maxAge} (Sorted by Age) ===");
                DisplayPersons(filteredPersons);
            }
            else
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
            }
            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }


        public void ShowAllPersonsInteractive()
        {
            Console.WriteLine("\n=== All Persons ===");
            var allPersons = TryGetAllPersons();
            DisplayPersons(allPersons);
            Console.WriteLine("\nPress Enter to return.");
            Console.ReadLine();
        }

        private void DisplayPersons(List<Person> persons)
        {
            if (persons.Count == 0)
            {
                Console.WriteLine("No persons found.");
            }
            else
            {
                foreach (var person in persons)
                {
                    Console.WriteLine(person);
                }
            }
        }
    }
}