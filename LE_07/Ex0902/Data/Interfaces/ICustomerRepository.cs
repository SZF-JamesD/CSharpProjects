using Ex0902.Data.DTOs;
using Ex0902.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ex0902.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetByIdAsync(int id);
        Task<int> CreateCustomerAsync(CreateCustomerDto dto, int createdBy);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(int id);
    }
}
