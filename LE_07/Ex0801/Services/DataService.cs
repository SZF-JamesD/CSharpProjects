using DBLib;
using Ex0801.Interfaces;
using Ex0801.Models;
using MvvmUtilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<int?> UserExistsAsync(string username, string password)
        {
            try
            {
                var sql = "select user_id from users where username = @username and password = @password";
                var parameters = new Dictionary<string, object>
                {
                    {"username", username },
                    {"password", password }
                };
                
                var results = await _dbService.GetAsync<int?>(sql, parameters, reader => reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0));
                foreach (var item in results)
                {
                    Console.WriteLine(item.ToString());
                }
                return results.FirstOrDefault();
            }
            catch (NullReferenceException ex) 
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var sql = "select * from customers";
            var parameters = new Dictionary<string, object>();

            var customers = await _dbService.GetAsync<Customer>(sql, parameters, reader =>
            {
                var customerDict = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    customerDict[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                }

                return Customer.FromDict(customerDict);
            });

            return customers;
        }

        public async Task<int> AddNewUserAsync(string username, string password)
        {
            var tableName = "users";

            var existingUser = await _dbService.GetAsync<int>("select * from users where username = @username",
                new Dictionary<string, object> { {"username", username } },
                reader => Convert.ToInt32(reader["Count(*)"])
                );

            if (existingUser.FirstOrDefault() > 0)
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


        public async Task<Customer> AddNewCustomerAsync(Customer customer)
        {
            var existingCustomer = await _dbService.GetAsync<int>("select * from customers where email = @email",
                new Dictionary<string, object> { {"email", customer.Email} },
                reader => Convert.ToInt32(reader["count(*)"])
                );

            if (existingCustomer.FirstOrDefault() > 0)
            {
                throw new InvalidOperationException("A customer with this email already exists.");
            }

            await _dbService.AddAsync("customers", customer.ToDict());

            var insertedId = await _dbService.GetAsync<int>(
                "SELECT LAST_INSERT_ID()",
                new Dictionary<string, object>(),
                reader => Convert.ToInt32(reader[0])
            );

                customer.CustId = insertedId.FirstOrDefault();
                return customer;
        }

        public async Task<Customer> EditCustomerAsync(Dictionary<string, object> data)
        {
            if (!data.ContainsKey("customer_id"))
                throw new ArgumentException("Customer ID is required to update a customer");

            int customer_id = Convert.ToInt32(data["customer_id"]);

            var whereClause = "customer_id = @customer_id";
            var whereParams = new Dictionary<string, object> { { "customer_id", customer_id} };

            var updateData = new Dictionary<string, object>(data);
            updateData.Remove("customer_id");

            var updatedDict = await _dbService.UpdateAsync("customers", updateData, whereClause, whereParams);

            return updatedDict != null ? Customer.FromDict(updatedDict) : null;
        }


        public async Task DeleteCustomerAsync(int? customerId)
        {
            var tableName = "customers";
            var keyColumn = "customer_id";
            var keyValue = customerId;

            if (await _dbService.RemoveAsync(tableName, keyColumn, keyValue)) _dialogService.ShowMessage("User successfully Deleted", "Success");
            else _dialogService.ShowError("An error occured during deletion", "Error");
        }
    }
}



