using Ex0801.Models;
using Ex0801.Services;
using MvvmUtilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using ValidationLib;
using Ex0801.Interfaces;
using MvvmUtilities.Interfaces;
using System.Configuration;

namespace Ex0801.ViewModels
{
    public class AddCustomerViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<Customer> _customers;
        private int _loggedEmployeeId;

        private int? _custId;

        public int? CustId
        {
            get => _custId;
            set => SetProperty(ref _custId, value);
        }


        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                SetProperty(ref _firstName, value);
                ((AsyncRelayCommand)AddCustomerCommand).RaiseCanExecuteChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                SetProperty(ref _lastName, value);
                ((AsyncRelayCommand)AddCustomerCommand).RaiseCanExecuteChanged();
            }
            }

        private string _street;
        public string Street
        {
            get => _street;
            set => SetProperty(ref _street, value);
        }

        private string _houseNo;
        public string HouseNo
        {
            get => _houseNo;
            set => SetProperty(ref _houseNo, value);
        }

        private int _postCode;
        public int PostCode
        {
            get => _postCode;
            set => SetProperty(ref _postCode, value);
        }

        private string _city;
        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        

        public ICommand AddCustomerCommand { get; }

        public AddCustomerViewModel(IDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            _dialogService = dialogService;

            AddCustomerCommand = new AsyncRelayCommand(async () => await AddCustomerAsync(), 
                canExecute: () => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName));
        }   

        private bool AreFieldsValid()
        {
            var fullName = $"{FirstName} {LastName}";
            var rawAddress = $"{Street} {HouseNo} {PostCode} {City}";

            var validations = new List<Func<ValidationResult<string>>>
                {
                    () => ValidationUtil.IsValidFullName($"{FirstName} {LastName}"),
                    () => ValidationUtil.IsValidEmail(Email)
                };

            foreach (var validate in validations)
            {
                var result = validate();

                if (!result.IsValid)
                {
                    _dialogService.ShowError(result.ErrorMessage);
                }
            }

            if (string.IsNullOrEmpty(Street) || string.IsNullOrEmpty(HouseNo) || string.IsNullOrEmpty(City) || !Regex.IsMatch(PostCode.ToString() ?? string.Empty, @"^\d{4}$"))
            {
                _dialogService.ShowError("Invalid Address", "Error");
                return false;
            }

            return true;
        }
  
        private async Task AddCustomerAsync()
        {
            if (AreFieldsValid())
            {
                var newcustomer = new Customer
                (
                    FirstName,
                    LastName,
                    Street,
                    HouseNo,
                    PostCode,
                    City,
                    Email,
                    _loggedEmployeeId,
                    null
                );

                await _dataService.AddNewCustomerAsync(newcustomer);

                _customers.Add(newcustomer);

                _dialogService.ShowMessage("Customer created successfully!", "Success");

            }
        }

        public void SetContext(ObservableCollection<Customer> customers, int loggedEmployeeId)
        {
            _customers = customers;
            _loggedEmployeeId = loggedEmployeeId;
        }
        
    }
}
