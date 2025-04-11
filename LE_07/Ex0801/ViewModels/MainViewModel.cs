using Ex0801.Interfaces;
using Ex0801.Models;
using Ex0801.Views;
using Microsoft.Extensions.DependencyInjection;
using MvvmUtilities;
using MvvmUtilities.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Ex0801.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand ToAddCustomerCommand { get; }
        public ICommand ToEditCustomerCommand { get; }
        public ICommand DeleteCustomerCommand { get; }
        public ICommand ExitCommand { get; }
        public ObservableCollection<Customer> Customers { get; set; }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set { SetProperty(ref _selectedCustomer, value); }
        }

        public int LoggedEmployeeId { get; set; }

        private readonly IDataService _dataService;
        private readonly IDialogService _dialogService;

        public MainViewModel(IDataService dataService, IDialogService dialogService)
        {
            _dataService = dataService;
            _dialogService = dialogService;
            Customers = new ObservableCollection<Customer>();

            ToAddCustomerCommand = new RelayCommand(OpenAddcustomerWindow);
            ToEditCustomerCommand = new RelayCommand<Customer>(customer => { var parameters = (Customers, customer); OpenEditCustomerWindow(parameters); });
            DeleteCustomerCommand = new RelayCommand<Customer>(DeleteCustomer);
            ExitCommand = new RelayCommand(ExitProgram);

            LoadCustomers();
        }

        private async void LoadCustomers()
        {
            try
            {
                var customersList = await _dataService.GetAllCustomersAsync();
                Customers.Clear();
                foreach (var customer in customersList)
                {
                    Customers.Add(customer);
                }
            }
            catch (Exception ex)
            {
                _dialogService.ShowError("Error loading customers: " + ex.Message);
            }
        }

        private void OpenAddcustomerWindow()
        {
            var addCustomerView =  App.ServiceProvider.GetRequiredService<AddCustomerView>();
            if (addCustomerView.DataContext is AddCustomerViewModel vm)
            {
                vm.SetContext(Customers, LoggedEmployeeId);
            }
            addCustomerView.Show();
        }


        private void OpenEditCustomerWindow((ObservableCollection<Customer> customers, Customer customer) parameters)
        {
            if (parameters.Item2 != null)
            {
                var editCustomerView = App.ServiceProvider.GetRequiredService<EditCustomerView>();
                if (editCustomerView.DataContext is EditCustomerViewModel vm)
                {
                    vm.SetContext(parameters.Item1, parameters.Item2);
                }
                editCustomerView.Show();
            }
            else _dialogService.ShowError("You must have a customer selected!", "Error");
        }

        private void DeleteCustomer(Customer customer)
        {
            if (customer != null)
            {
                Customers.Remove(customer);
            }
            else _dialogService.ShowError("You must have a customer selected!", "Error");
        }

        private void ExitProgram()
        {
            Application.Current.Shutdown();
        }
    }
}
