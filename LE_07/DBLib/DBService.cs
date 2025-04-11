using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace DBLib
{
    public class DBService
    {
        private readonly Func<DbConnection> _connectionFactory;

        public DBService(Func<DbConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
        }

        private DbConnection GetOpenConnection()
        {
            var conn = _connectionFactory();
            if (conn.State != System.Data.ConnectionState.Open)
                conn.Open();
            return conn;
        }

        public async Task<int> AddAsync(string tableName, Dictionary<string, object> data)
        {
            if (string.IsNullOrEmpty(tableName)) throw new ArgumentException("Table name cannot be null or empty");
            if (data == null || data.Count == 0) throw new ArgumentException("Data cannot be null or empty");

            var columns = string.Join(", ", data.Keys);
            var values = string.Join(", ", data.Keys.Select(p => $"@{p}"));
            var sql = $"INSERT INTO {tableName} ({columns}) VALUES ({values}); SELECT LAST_INSERT_ID();";

            return await ExecuteScalarAsync<int>(sql, cmd =>
            {
                foreach (var pair in data)
                {
                    cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value ?? DBNull.Value);
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


        public async Task<List<T>> GetAsync<T>(string sql, Dictionary<string, object> parameters, Func<MySqlDataReader, T> mapper)
        {
            return await ExecuteQueryAsync(sql, cmd =>
            {
                foreach (var pair in parameters)
                {
                    cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value ?? DBNull.Value);
                }
            }, mapper);
        } //refactored to just one method, for one result just need to call FirstOrDefault();

        public async Task<Dictionary<string, object>> UpdateAsync(string tableName, Dictionary<string, object> data, string whereClause, Dictionary<string, object> whereParameters)
        { 
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("Table name cannot be null or empty.");
            if (data == null || data.Count == 0) throw new ArgumentException("Data cannot be null or empty.");
            if (string.IsNullOrWhiteSpace(whereClause)) throw new ArgumentException("WHERE clause cannot be null or empty.");

            var setClause = string.Join(", ", data.Keys.Select(k => $"{k} = @{k}"));

            var sql = $"UPDATE {tableName} SET {setClause} WHERE {whereClause}";

            await ExecuteNonQueryAsync(sql, cmd =>
            {
                foreach (var pair in data)
                    cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value);

                if (whereParameters != null)
                {
                    foreach (var pair in whereParameters)
                        cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                }
            });

            string selectSql = $"SELECT * FROM {tableName} WHERE {whereClause} LIMIT 1;";
            var updated = await ExecuteQueryAsync(selectSql, cmd =>
            {
                if (whereParameters != null)
                {
                    foreach (var pair in whereParameters)
                        cmd.Parameters.AddWithValue($"@{pair.Key}", pair.Value);
                }
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

            return updated.FirstOrDefault();
        }

        private async Task<int> ExecuteNonQueryAsync(string sql, Action<MySqlCommand> paramSetter)
        {
            using (var connection = GetOpenConnection())
            using (var cmd = new MySqlCommand(sql, (MySqlConnection)connection))
            {
                paramSetter(cmd);
                return await cmd.ExecuteNonQueryAsync();
            }
        }

        private async Task<T> ExecuteScalarAsync<T>(string sql, Action<MySqlCommand> paramSetter)
        {
            using (var connection = GetOpenConnection())
            using (var cmd = new MySqlCommand(sql, (MySqlConnection)connection))
            {
                paramSetter(cmd);
                object result = await cmd.ExecuteScalarAsync();
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        private async Task<List<T>> ExecuteQueryAsync<T>(string sql, Action<MySqlCommand> paramSetter, Func<MySqlDataReader, T> mapper)
        {
            var results = new List<T>();
            using (var connection = GetOpenConnection())
            using (var cmd = new MySqlCommand(sql, (MySqlConnection)connection))
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
