using System;

namespace Ex0301.Models
{
    public class Address : Person
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string PhoneNumber { get; set; }

        public Address(string firstName, string lastName, string street, string city, string postalCode, string phoneNumber) 
            : base(firstName, lastName)
        {
            Street = street;
            City = city;
            PostalCode = postalCode;
            PhoneNumber = phoneNumber;
        }

        public override string DisplayInfo()
        {
            return $"--Address Info--\nName: {FirstName} {LastName}\nAddress: {Street}, {City}, {PostalCode}\nPhone Number: {PhoneNumber}";
        }

        public override string ToString()
        {
            return $"Name: {FirstName} {LastName}, Address: {Street}, {City}, {PostalCode}, Phone: {PhoneNumber}";
        }
    }
}
