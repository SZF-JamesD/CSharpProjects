using Ex0603.Models;
using Ex0603.Services;
using MvvmUtilities;
using MvvmUtilities.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using System.Threading.Tasks;
using Ex0603.Views;

namespace Ex0603.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly AuthenticationService _authenticationService;
        private readonly IDialogService _dialogService;
        private readonly InvoiceService _invoiceService;

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set => SetProperty(ref _currentView, value);
        }

        public ObservableCollection<Invoice> Invoices { get; set; } = new ObservableCollection<Invoice>();
        
        public ICommand ShowLoginViewCommand { get; }
        public ICommand CreateInvoiceCommand { get; }
        public ICommand OpenInvoiceCommand { get; }

        private string _customerName;
        private string _customerNumber;
        private string _productName;
        private string _productPrice;
        private string _quantity;

        public string CustomerName
        {
            get => _customerName;
            set => SetProperty(ref _customerName, value);
        }

        public string CustomerNumber
        {
            get => _customerNumber;
            set => SetProperty(ref _customerNumber, value);
        }

        public string ProductName
        {
            get => _productName;
            set => SetProperty(ref _productName, value);
        }

        public string ProductPrice
        {
            get => _productPrice;
            set => SetProperty(ref _productPrice, value);
        }

        public string Quantity
        {
            get => _quantity; 
            set => SetProperty(ref _quantity, value);
        }



        public MainViewModel(AuthenticationService authenticationService, IDialogService dialogService)
        {
            _authenticationService = authenticationService;
            _dialogService = dialogService;
            _invoiceService = new InvoiceService("/../../Data/");

            CurrentView = new LoginViewModel(_authenticationService, _dialogService, OnLoginSuccess);

            ShowLoginViewCommand = new RelayCommand(() => CurrentView = new LoginViewModel(_authenticationService, _dialogService, OnLoginSuccess));
            CreateInvoiceCommand = new RelayCommand(CreateInvoice);
            OpenInvoiceCommand = new RelayCommand<Invoice>(OpenInvoice);

            System.Windows.Application.Current.Exit += async (s, e) => await SaveInvoices();
        }

        private void OnLoginSuccess()
        {
            CurrentView = new MainView();
            LoadInvoices();
        }

        private void LoadInvoices()
        {
            Invoices.Clear();
            foreach (var invoice in _invoiceService.LoadInvoices())
            {
                Invoices.Add(invoice);
            }           
        }

        
        private void CreateInvoice()
        {
            if (string.IsNullOrWhiteSpace(CustomerName) || string.IsNullOrWhiteSpace(ProductName) || string.IsNullOrWhiteSpace(ProductPrice) || string.IsNullOrWhiteSpace(Quantity))
            {
                _dialogService.ShowError("All Fields must be Filled!", "Missing Information");
                return;
            }

            if (!decimal.TryParse(ProductPrice, out decimal price) || !int.TryParse(Quantity, out int quantity))
            {
                _dialogService.ShowError("Invalid price or quanitity!", "Input Error");
                return;
            }
            var customer = Customer.GetOrCreateCustomer(CustomerName, CustomerNumber);
            var product = new Product(ProductName, price);
            var newInvoice = new Invoice(customer, product, quantity);

            Invoices.Add(newInvoice);

            _dialogService.ShowMessage("Invoice created successfully!", "Success");

            CustomerName = string.Empty;
            CustomerNumber = string.Empty;
            ProductName = string.Empty;
            ProductPrice = string.Empty;
            Quantity = string.Empty;
        }

        private void OpenInvoice(Invoice selectedInvoice)
        {
            if (selectedInvoice == null) return;
            
            var invoiceViewModel = new InvoiceViewModel(selectedInvoice);
            var invoiceWindow = new InvoiceView()
            {
                DataContext = invoiceViewModel
            };

            invoiceWindow.Show();
        }

        private async Task SaveInvoices()
        {
            foreach (var invoice in Invoices)
            {
                await _invoiceService.SaveInvoiceAsync(invoice);
            }
        }
    }
}

