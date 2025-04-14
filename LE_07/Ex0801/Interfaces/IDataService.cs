using Ex0801.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ex0801.Interfaces
{
    public interface IDataService
    {
        Task<int?> UserExistsAsync(string username, string password);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<int> AddNewUserAsync(string username, string password);
        Task<Customer> AddNewCustomerAsync(Customer customer);
        Task<Customer> EditCustomerAsync(Dictionary<string, object> data);
        Task DeleteCustomerAsync(int? customerId);
    }
}
