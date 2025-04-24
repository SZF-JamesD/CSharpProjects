using DBLib;
using Ex0902.Data.DTOs;
using Ex0902.Data.Interfaces;

namespace Ex0902.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DBService _dbService;
        public UserRepository(DBService dbService) => _dbService = dbService;


        public async Task<int?> AuthenticateAsync(string username, string password)
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
            
        

        public async Task<int?> CreateUserAsync(UserDto dto)
        {
            var existingUser = await _dbService.GetAsync<int>("select * from users where username = @username",
                new Dictionary<string, object> { { "username", dto.Username } },
                reader => Convert.ToInt32(reader[0])
                );

            if (existingUser.FirstOrDefault() > 0)
            {
                return null;
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
