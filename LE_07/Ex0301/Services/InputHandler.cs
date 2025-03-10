using Ex0301.Interfaces;
using System;
using ValidationLib;

namespace Ex0301.Services
{
    public class InputHandler
    {
        private IAddressBook _addressBookService;

        public InputHandler(IAddressBook addressBookService)
        {
            _addressBookService = addressBookService;
        }

        public void HandleAddAddress()
        {
            (string firstName, string lastName) = GetValidatedName();
            (string street, string postalCode, string city) = GetValidatedAddress();
            string phoneNumber = GetValidatedPhoneNumber();
            _addressBookService.AddAddress(firstName, lastName, street, city, postalCode, phoneNumber);
        }

        private (string FirstName, string LastName) GetValidatedName()
        {
            while (true)
            {
                Console.Write("Enter first and last name: ");
                string name = Console.ReadLine()?.Trim();

                var validationResult = ValidationUtil.IsValidFullName(name);
                if (validationResult.IsValid)
                {
                    string formattedName = validationResult.Value;
                    string[] nameParts = formattedName.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    string firstName = nameParts[0];
                    string lastName = nameParts[1];
                    return (firstName, lastName);

                }
                else
                {
                    Console.WriteLine("Error: " + validationResult.ErrorMessage);
                }
            }
        }

        public (string Street, string PostCode, string City) GetValidatedAddress()
        {
            while (true)
            {
                Console.Write("Enter your address (Street Nr., Post Code, City)");
                string address = Console.ReadLine()?.Trim();

                var validationResult = ValidationUtil.IsValidAddress(address);
                if (validationResult.IsValid)
                {
                    string street = $"{validationResult.Value.Street} {validationResult.Value.Number}" + (string.IsNullOrWhiteSpace(validationResult.Value.Apartment) ? "" : $" {validationResult.Value.Apartment}");
                    string postalCode = $"{validationResult.Value.PostalCode}"; 
                    string city = $"{validationResult.Value.City}";
                    return (street, postalCode, city);
                }
                else
                {
                    Console.WriteLine("Error: " + validationResult.ErrorMessage);
                }
            }
        }

        public string GetValidatedPhoneNumber()
        {
            while (true)
            {
                Console.Write("Enter your phone number with area code (e.g. +43660 ....): ");
                string phoneNumber = Console.ReadLine()?.Trim();

                var validationResult = ValidationUtil.IsValidPhoneNumber(phoneNumber);
                if (validationResult.IsValid)
                {
                    return validationResult.Value;
                }
                else
                {
                    Console.WriteLine("Error: " + validationResult.ErrorMessage);
                }
            }
        }

        public void HandleRemoveAddress()
        {
            string phoneNumber = GetValidatedPhoneNumber();
            _addressBookService.RemoveAddress(phoneNumber);
        }

        public void HandleFindAddress()
        {
            string phonenumber = GetValidatedPhoneNumber();
            _addressBookService.FindAddress(phonenumber);
        }
    }
}
