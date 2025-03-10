using Ex0301.Interfaces;
using Ex0301.Models;
using System;
using System.Collections.Generic;
using ValidationLib;

namespace Ex0301.Services
{
    public class AddressBookService : IAddressBook
    {
        private readonly Dictionary<string, Address> _addresses = new Dictionary<string, Address>();

        public void AddAddress(string firstName, string lastName, string street, string city, string postalCode, string phoneNumber)
        {
            if (_addresses.ContainsKey(phoneNumber))
            {
                Console.WriteLine("Address already registered under this phone number.");
                return;
            }

            Address newAddress = new Address(firstName, lastName, street, city, postalCode, phoneNumber);
            _addresses.Add(phoneNumber, newAddress);
            Console.WriteLine("Address added successfully!");             
        }

        public void RemoveAddress(string phoneNumber)
        {
            if (!_addresses.ContainsKey(phoneNumber))
            {
                Console.WriteLine("No address related to this phone number.");
                return;
            }

            _addresses.Remove(phoneNumber);
            Console.WriteLine("Address removed successfully");
        }

        public void FindAddress(string phoneNumber)
        {
            Address address = _addresses.ContainsKey(phoneNumber) ? _addresses[phoneNumber] : null;
            if (address != null)
            {
                Console.WriteLine(address.ToString());
                return;
            }
            Console.WriteLine("No address with this phone number found.");
            return;
        }

        public void DisplayAllAddresses()
        {
            if (_addresses.Count == 0)
            {
                Console.WriteLine("No addresses found.");
                return; 
            }

            foreach (var address in _addresses.Values)
            {
                Console.WriteLine(address.DisplayInfo());
            }
        }
    }
}
