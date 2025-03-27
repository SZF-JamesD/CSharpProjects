using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Security.Cryptography;

namespace DBLib
{
    public class DBService
    {
        private readonly DbConnection _connection;

        public DBService(DbConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException("Connection must be open");
            }
        }

        public async Task<int> AddAsync(string tableName, Dictionary<string, object> data)
        {
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentException("Table name cannot be null or empty");
            if (data == null || data.Count == 0) throw new ArgumentException("Data cannot be null or empty");

            var columns = string.Join(", ", data.Keys);
            var values = string.Join(", ", data.Keys.Select(p => $"@{p}"));

            var sql = $"INSERT INTO {tableName} ({columns}) VALUES {values}; SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, cmd =>
            {
                foreach (var pair in data)
                {
                    cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                }
            });
        }

        public async Task<bool> RemoveAsync(string tableName, string keyColumn, object keyValue)
        {
            var sql = $"DELETE FROM {tableName} WHERE {keyColumn} = @VALUE";
            var rowsAffected = await ExecuteNonQueryAsync(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@value", keyValue);
            });
            return rowsAffected > 0;
        }

        public async Task<Dictionary<string, object>> GetOneAsync(string tableName, string keyColumn, object keyValue)
        {
            var sql = $"SELECT * FROM {tableName} WHERE {keyColumn} = @value LIMIT 1";

            var result = await ExecuteQueryAsync(sql, cmd =>
            {
                cmd.Parameters.AddWithValue("@value", keyValue);
            },
            reader =>
            {
                var dict = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict[reader.GetName(i)] = reader.GetValue(i);
                }
                return dict;
            });
            return result.Count > 0 ? result[0] : null;
        }

        public async Task<List<Dictionary<string, object>>> GetAllAsync(string tableName)
        {
            var sql = $"SELECT * FROM {tableName}";

            return await ExecuteQueryAsync(sql, cmd => { }, reader =>
            {
                var dict = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dict[reader.GetName(i)] = reader.GetValue(i);
                }
                return dict;
            });
        }

        public void Close()
        {
            if (_connection.State != System.Data.ConnectionState.Closed) _connection.Close();
        }

        public void Dispose()
        {
            Close();
            _connection.Dispose();
        }

        private async Task<int> ExecuteNonQueryAsync(string sql, Action<MySqlCommand> paramSetter)
        {
            using (var cmd = new MySqlCommand(sql, (MySqlConnection)_connection))
            {
                paramSetter(cmd);
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task<T> ExecuteScalarAsync<T>(string sql, Action<MySqlCommand> paramSetter)
        {
            using (var cmd = new MySqlCommand(sql, (MySqlConnection)_connection))
            {
                paramSetter(cmd);
                object result = await cmd.ExecuteScalarAsync();
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        private async Task<List<T>> ExecuteQueryAsync<T>(string sql, Action<MySqlCommand> paramSetter, Func<MySqlDataReader, T> mapper)
        {
            var results = new List<T>();
            using (var cmd = new MySqlCommand(sql, (MySqlConnection)_connection))
            {
                paramSetter(cmd);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        results.Add(mapper((MySqlDataReader)reader));
                    }
                }
            }
            return results;
        }
    }
}
