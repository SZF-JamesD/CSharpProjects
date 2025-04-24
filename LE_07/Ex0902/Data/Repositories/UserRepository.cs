using Ex0902.Data.Interfaces;
using Ex0902.Models;
using DBLib;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ex0902.Data.DTOs;

namespace Ex0902.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBService _dbService;
        public UserRepository(DBService dbService) => _dbService = dbService;
        public async Task<int?> AuthenticateAsync(string username, string password)
        {
            try
            {
                var sql = "select user_id from users where username = @username and password = @password";
                var rows = await _dbService.GetAsync<int?>(sql, new Dictionary<string, object>
                {
                    ["username"] = username,
                    ["password"] = password
                },
                reader => reader.IsDBNull(0) ? (int?)null : reader.GetInt32(0));
                return rows.FirstOrDefault();
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
        }

        public async Task<int> CreateUserAsync(UserDto dto)
        {
            var existingUser = await _dbService.GetAsync<int>("select * from users where username = @username",
                new Dictionary<string, object> { { "username", dto.Username } },
                reader => Convert.ToInt32(reader["Count(*)"])
                );

            if (existingUser.FirstOrDefault() > 0)
            {
                throw new InvalidOperationException("A user with this username already exists.");
            }

            var data = new Dictionary<string, object>
            {
                ["username"] = dto.Username,
                ["password"] = dto.Password
            };
            return await _dbService.AddAsync("users", data);
        }
    }
}
