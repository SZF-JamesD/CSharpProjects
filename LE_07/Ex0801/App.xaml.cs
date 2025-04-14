using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using DBLib;                      
using Ex0801.Services;            
using MvvmUtilities;              
using Ex0801.ViewModels;          
using Ex0801.Views;
using MvvmUtilities.Interfaces;
using Ex0801.Interfaces;
using System.Collections.ObjectModel;
using Ex0801.Models;
using System.Data.Common;

namespace Ex0801
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            //InitializeComponent();

            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();


            try
            {
                string databaseName = "gold_digger";
                string createTablesSql = @"
                    CREATE TABLE IF NOT EXISTS users (
                        user_id INT PRIMARY KEY AUTO_INCREMENT,
                        username VARCHAR(50) NOT NULL,
                        password VARCHAR(50) NOT NULL
                    );
                    CREATE TABLE IF NOT EXISTS customers (
                        customer_id INT PRIMARY KEY AUTO_INCREMENT,
                        first_name VARCHAR(50) NOT NULL,
                        last_name VARCHAR(50) NOT NULL,
                        street VARCHAR(100),
                        house_no VARCHAR(10),
                        post_code INT,
                        city VARCHAR(50),
                        email VARCHAR(100),
                        created_by INT,
                        FOREIGN KEY (created_by) REFERENCES users(user_id)
                    );";

                DBInitializer.CreateDatabaseAndTables(databaseName, createTablesSql);
            }
            catch (Exception ex)
            {
                var dialogService = ServiceProvider.GetRequiredService<IDialogService>();
                dialogService.ShowError("Error during database setup: " + ex.Message, "Error");
            }
            var mainWindowViewModel = ServiceProvider.GetRequiredService<MainWindowViewModel>();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();

            base.OnStartup(e);
        }
        
            
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<Func<DbConnection>>(_ => () => DBConnection.GetConnection());
            services.AddTransient<Func<DbConnection>>(_ => () => DBConnection.GetConnection("gold_digger"));
            services.AddTransient<DBService>();

            services.AddSingleton<IDialogService, DialogService>();
            services.AddSingleton<IDataService, DataService>();

            services.AddSingleton<ObservableCollection<Customer>>();

            services.AddSingleton<MainWindowViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<AddCustomerViewModel>();
            services.AddTransient<AddUserViewModel>();
            services.AddTransient<EditCustomerViewModel>();
            services.AddTransient<CustomerDetailViewModel>();

            services.AddSingleton<MainWindow>();
            services.AddTransient<LoginView>();
            services.AddTransient<MainView>();
            services.AddTransient<AddCustomerView>();
            services.AddTransient<AddUserView>();
            services.AddTransient<EditCustomerView>();
            services.AddTransient<CustomerDetailView>();
        }
    }
}
