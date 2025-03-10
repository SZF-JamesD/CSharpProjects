using Ex0301.Models;
using System.Collections.Generic;

namespace Ex0301.Interfaces
{
    public interface IAddressBook
    {
        void AddAddress(string firstName, string lastName, string street, string city, string postCode, string phoneNumber);

        void RemoveAddress(string phoneNumber);

        void FindAddress(string phoneNumber);

        void DisplayAllAddresses();
    }
}
