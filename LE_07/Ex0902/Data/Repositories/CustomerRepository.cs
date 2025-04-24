using Ex0902.Data.Interfaces;
using DBLib;
using Ex0902.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing.Printing;
using Ex0902.Data.DTOs;

namespace Ex0902.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DBService _dbService;
        public CustomerRepository(DBService dbService) => _dbService = dbService;

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            var rows = await _dbService.GetAsync<Dictionary<string, object>>(
                "SELECT * FROM customers", new(), reader =>
                {
                    var d = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        d[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    return d;
                });
            return rows.Select(Customer.FromDict);
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM customers WHERE customer_id = @id";
            var rows = await _dbService.GetAsync<Dictionary<string, object>>(sql,
                new Dictionary<string, object> { { "id", id } }, reader =>
                {
                    var d = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                        d[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    return d;
                });
            var dict = rows.FirstOrDefault();
            return dict == null ? null : Customer.FromDict(dict);
        }

        public async Task<int> CreateCustomerAsync(CreateCustomerDto dto, int createdBy)
        {
            var data = dto.ToDict(createdBy);
            return await _dbService.AddAsync("customers", data);
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            var whereParams = new Dictionary<string, object> { { "customer_id", customer.CustomerId } };
            var data = customer.ToDict();

            var updatedDict = await _dbService.UpdateAsync("customers", data, "customer_id = @customer_id", whereParams);
            return updatedDict != null;
        }

        public Task<bool> DeleteCustomerAsync(int id) =>
            _dbService.RemoveAsync("customers", "customer_id", id);
    }
}
