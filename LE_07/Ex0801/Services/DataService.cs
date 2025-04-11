using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBLib;
using Ex0801.Models;
using MvvmUtilities;
using Ex0801.Interfaces;
using MvvmUtilities.Interfaces;

namespace Ex0801.Services
{
    public class DataService : IDataService
    {
        private readonly DBService _dbService;
        private readonly IDialogService _dialogService;

        public DataService(DBService dbService, IDialogService dialogService)
        {
            _dbService = dbService;
            _dialogService = dialogService;
        }

        public async Task<bool> UserExistsAsync(string username, string password)
        {
            try
            {
                var tableName = "users";
                var keyColumn = "username and password";
                var keyValue = username + " and " + password;

                if (await _dbService.GetOneAsync(tableName, keyColumn, keyValue) != null)
                {
                    return true;
                }
                return false;
            }
            catch (NullReferenceException ex) 
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var dbName = "customers";

            var data = await _dbService.GetAllAsync(dbName);

            return data.Select(dict => Customer.FromDict(dict));
        }

        public async Task<int> AddNewUserAsync(string username, string password)
        {
            var tableName = "users";

            var existingUser = await _dbService.GetOneAsync("users", "username", username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("A user with this username already exists.");
            }

            var data = new Dictionary<string, object>
            {
                { "username", username },
                { "password", password }
            };

            return await _dbService.AddAsync(tableName, data);
        }


        public async Task<int> AddNewCustomerAsync(Customer customer)
        {
            var existingCustomer = await _dbService.GetOneAsync("customers", "email", customer.Email);
            if (existingCustomer != null)
            {
                throw new InvalidOperationException("A customer with this email already exists.");
            }

            return await _dbService.AddAsync("customers", customer.ToDict());
        }

        public async Task<Customer> EditCustomerAsync(Dictionary<string, object> data)
        {
            if (!data.ContainsKey("CustId"))
                throw new ArgumentException("Customer ID is required to update a customer");

            int custId = Convert.ToInt32(data["CustId"]);

            var whereClause = "CustId = @CustId";
            var whereParams = new Dictionary<string, object> { { "CustId", custId } };

            var updateData = new Dictionary<string, object>(data);
            updateData.Remove("CustId");

            var updatedDict = await _dbService.UpdateAsync("customers", updateData, whereClause, whereParams);

            return updatedDict != null ? Customer.FromDict(updatedDict) : null;
        }


        public async Task DeleteCustomerAsync(int customerId)
        {
            var tableName = "customers";
            var keyColumn = "customer_id";
            var keyValue = customerId;

            if (await _dbService.RemoveAsync(tableName, keyColumn, keyValue)) _dialogService.ShowMessage("User successfully Deleted", "Success");
            else _dialogService.ShowError("An error occured during deletion", "Error");
        }
    }
}



