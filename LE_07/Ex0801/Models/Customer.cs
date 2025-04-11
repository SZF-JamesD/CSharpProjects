using System;
using System.Collections.Generic;

namespace Ex0801.Models
{
    public class Customer
    {
        public int? CustId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string HouseNo { get; set; }
        public int PostCode { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int CreatedBy { get; set; }


        public Customer(string firstName, string lastName, string street, string houseNo, int postCode, string city, string email, int createdBy, int? custId = null)
        {
            CustId = custId;
            FirstName = firstName;
            LastName = lastName;
            Street = street;
            HouseNo = houseNo;
            PostCode = postCode;
            City = city;
            Email = email;
            CreatedBy = createdBy;
        }

        public Dictionary<string, object> ToDict()
        {
            return new Dictionary<string, object>
            {
                { "customer_id", CustId },
                { "first_name", FirstName },
                { "last_name", LastName },
                { "street", Street },
                { "house_no", HouseNo },
                { "post_code", PostCode },
                { "city", City },
                { "email", Email },
                { "created_by", CreatedBy }
            };
        }

        public static Customer FromDict(Dictionary<string, object> dict)
        {
            if (dict == null) return null;

            return new Customer(
                dict["first_name"]?.ToString(),
                dict["last_name"]?.ToString(),
                dict["street"]?.ToString(),
                dict["house_no"]?.ToString(),
                Convert.ToInt32(dict["post_code"]),
                dict["city"]?.ToString(),
                dict["email"]?.ToString(),
                Convert.ToInt32(dict["created_by"]),
                Convert.ToInt32(dict["customer_id"])
            );
        }
    }
}
