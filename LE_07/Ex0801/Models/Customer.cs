using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Ex0801.Models
{
    public class Customer : INotifyPropertyChanged
    {
        private int? _custId;
        private string _firstName;
        private string _lastName;
        private string _street;
        private string _houseNo;
        private int _postCode;
        private string _city;
        private string _email;
        private int _createdBy;

        public int? CustId
        {
            get => _custId;
            set => SetProperty(ref _custId, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        public string Street
        {
            get => _street;
            set => SetProperty(ref _street, value);
        }

        public string HouseNo
        {
            get => _houseNo;
            set => SetProperty(ref _houseNo, value);
        }

        public int PostCode
        {
            get => _postCode;
            set => SetProperty(ref _postCode, value);
        }

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public int CreatedBy
        {
            get => _createdBy;
            set => SetProperty(ref _createdBy, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

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
