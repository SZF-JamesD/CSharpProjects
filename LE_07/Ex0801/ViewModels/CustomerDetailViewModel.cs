using Ex0801.Models;
using MvvmUtilities;

namespace Ex0801.ViewModels
{
    public class CustomerDetailViewModel : ViewModelBase
    {
        private Customer _customer;

        public Customer Customer
        {
            get => _customer;
            set => SetProperty(ref _customer, value);
        }

        public void SetContext(Customer customer)
        {
            Customer = customer;
        }
    }
}
