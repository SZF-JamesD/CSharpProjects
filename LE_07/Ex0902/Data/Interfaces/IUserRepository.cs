using Ex0902.Data.DTOs;
using Ex0902.Models;
using System.Threading.Tasks;

namespace Ex0902.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<int?> AuthenticateAsync(string username, string password);
        Task<int> CreateUserAsync(UserDto dto);
    }
}
