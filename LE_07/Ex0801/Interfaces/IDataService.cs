using Ex0801.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Ex0801.Interfaces
{
    public interface IDataService
    {
        Task<bool> UserExistsAsync(string username, string password);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<int> AddNewUserAsync(string username, string password);
        Task<int> AddNewCustomerAsync(Customer customer);
        Task<Customer> EditCustomerAsync(Dictionary<string, object> data);
        Task DeleteCustomerAsync(int customerId);
    }
}
