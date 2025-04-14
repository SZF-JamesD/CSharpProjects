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
        public ICommand ToCustomerDetailsCommand { get; }
        public ObservableCollection<Customer> Customers { get; set; }

        private Customer _selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return _selectedCustomer; }
            set 
            { 
                SetProperty(ref _selectedCustomer, value);
            }
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
            ToEditCustomerCommand = new RelayCommand(OpenEditCustomerWindow);
            DeleteCustomerCommand = new RelayCommand(DeleteCustomer);
            ExitCommand = new RelayCommand(ExitProgram);
            ToCustomerDetailsCommand = new RelayCommand(OnCustomerDoubleClick);

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


        private void OpenEditCustomerWindow()
        {
            if (SelectedCustomer != null)
            {
                var editCustomerView = App.ServiceProvider.GetRequiredService<EditCustomerView>();
                if (editCustomerView.DataContext is EditCustomerViewModel vm)
                {
                    vm.SetContext(Customers, SelectedCustomer);
                }
                editCustomerView.Show();
            }
            else _dialogService.ShowError("You must have a customer selected!", "Error");
        }

        private void DeleteCustomer()
        {
            if (SelectedCustomer != null)
            {
                _dataService.DeleteCustomerAsync(SelectedCustomer.CustId);
                Customers.Remove(SelectedCustomer);
            }
            else _dialogService.ShowError("You must have a customer selected!", "Error");
        }

        private void OnCustomerDoubleClick()
        {
            if (SelectedCustomer != null)
            {
                var detailView = App.ServiceProvider.GetRequiredService<CustomerDetailView>();
                if (detailView.DataContext is CustomerDetailViewModel vm)
                {
                    vm.SetContext(SelectedCustomer);
                }
                detailView.Show();
            }
            else
            {
                _dialogService.ShowError("You must select a customer to view details", "No selection");
            }
        }

        private void ExitProgram()
        {
            Application.Current.Shutdown();
        }
    }
}
