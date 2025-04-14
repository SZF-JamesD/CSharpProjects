using Ex0801.Interfaces;
using Ex0801.Models;
using MvvmUtilities;
using MvvmUtilities.Interfaces;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ex0801.ViewModels
{
    public class EditCustomerViewModel : ViewModelBase
    {
        private string _firstName;
        private string _lastName;
        private string _street;
        private string _houseNo;
        private int _postCode;
        private string _city;
        private string _email;

        private Customer _customer;
        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService;
        private ObservableCollection<Customer> _customers;

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

        public int Postcode
        {
            get => _postCode;
            set => SetProperty(ref _postCode, value);
        }

        public string PostCodeText
        {
            get => _postCode.ToString();
            set
            {
                if (int.TryParse(value, out int parsed))
                {
                    Postcode = parsed;
                    OnPropertyChanged();
                }
            }
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

        public Customer Customer
        {
            get => _customer;
            set
            {
                if (SetProperty(ref _customer, value))
                {
                    FirstName = _customer?.FirstName;
                    LastName = _customer?.LastName;
                    Street = _customer?.Street;
                    HouseNo = _customer?.HouseNo;
                    Postcode = (int)_customer?.PostCode;
                    City = _customer?.City;
                    Email = _customer?.Email;
                }
            }
        }

        public ICommand SaveChangesCommand { get; }

        public EditCustomerViewModel(IDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            _dialogService = dialogService;


            SaveChangesCommand = new AsyncRelayCommand(async () => await SaveChangesAsync(), canExecute: () => _customer != null);
        }

        public void SetContext(ObservableCollection<Customer> customers, Customer customer)
        {
            _customers = customers;
            Customer = customer;
        }

        private async Task SaveChangesAsync()
        {
            var confirmation = _dialogService.AskUserConfirmation("Are you sure you want to make these changes?");

            if (confirmation)
            {
                _customer.FirstName = FirstName;
                _customer.LastName = LastName;
                _customer.Street = Street;
                _customer.HouseNo = HouseNo;
                _customer.PostCode = Postcode;
                _customer.City = City;
                _customer.Email = Email;

                var data = _customer.ToDict();

                await _dataService.EditCustomerAsync(data);

                var existingCustomer = _customers.FirstOrDefault(c => c.CustId == _customer.CustId);
                    if (existingCustomer != null)
                    {
                        existingCustomer.FirstName = _customer.FirstName;
                        existingCustomer.LastName = _customer.LastName;
                        existingCustomer.Street = _customer.Street;
                        existingCustomer.HouseNo = _customer.HouseNo;
                        existingCustomer.PostCode = _customer.PostCode;
                        existingCustomer.City = _customer.City;
                        existingCustomer.Email = _customer.Email;
                }
            }
        }
    }
}
