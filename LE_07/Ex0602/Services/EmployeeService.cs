using System.Threading.Tasks;

namespace Ex0602.Services
{
    internal class EmployeeService
    {
        private readonly DBService dbService;

        public EmployeeService(DBService dbService)
        {
            this.dbService = dbService;
        }

        public async Task<bool> ValidateEmployeeIdAsync(int employeeId)
        {
            return await dbService.EmployeeExistsAsync(employeeId);
        }
    }
}
