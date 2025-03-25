using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Ex0602.Models;

namespace Ex0602.Services
{
    public class DBService
    {
        private readonly MySqlConnection _connection;

        public DBService(MySqlConnection connection)
        {
            _connection = connection ?? throw new ArgumentNullException(nameof(connection));
            if (_connection.State != System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException("Connection must be open.");
            }
        }

        public async Task<bool> EmployeeExistsAsync(int employeeId)
        {
            string sql = "select count(*) from emyploees where employee_id = @id";
            int count = await ExecuteScalarAsync<int>(sql, async cmd =>
            {
                cmd.Parameters.AddWithValue("@id", employeeId);
                await Task.Delay(3000);
                await Task.CompletedTask;
            });
            return count > 0;
        }

        public async Task AddNoteAsync(string content, int employeeId)
        {
            string sql = @"insert into notes (content, date_created, employee_id)
                            values (@content, current_timestamp, @employeeId)";
            await ExecuteNonQueryAsync(sql, async cmd =>
            {
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@employeeId", employeeId);
                await Task.Delay(2000);
                await Task.CompletedTask;
            });
        }

        public async Task<List<Note>> GetAllNotesAsync()
        {
            string sql = "select * from notes order by note_id desc";
            return await ExecuteQueryAsync(sql, async cmd =>
            {
                await Task.CompletedTask;
            },
            reader =>
            {
                return new Note
                {
                    NoteID = reader.GetInt32("note_id"),
                    Content = reader.GetString("content"),
                    DateCreated = reader.GetDateTime("date_created"),
                    EmployeeID = reader.GetInt32("employee_id")
                };
            });
        }

        public async Task DeleteNoteAsync(int noteId)
        {
            string sql = "delete from notes where note_id = @id";
            await ExecuteNonQueryAsync(sql, async cmd =>
            {
                cmd.Parameters.AddWithValue("@id", noteId);
                await Task.CompletedTask;
            });
        }


        protected async Task ExecuteNonQueryAsync(string sql, Func<MySqlCommand, Task> stmtSetter)
        {
            using (var cmd = new MySqlCommand(sql, _connection))
            {
                await stmtSetter(cmd);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        protected async Task<T> ExecuteScalarAsync<T>(string sql, Func<MySqlCommand, Task> stmtSetter)
        {
            using (var cmd = new MySqlCommand(sql, _connection))
            {
                await stmtSetter(cmd);
                object result = await cmd.ExecuteScalarAsync();
                return (T)Convert.ChangeType(result, typeof(T));
            }
        }

        protected async Task<List<T>> ExecuteQueryAsync<T>(string sql, Func<MySqlCommand, Task> stmtSetter, Func<MySqlDataReader, T> mapper)
        {
            List<T> results = new List<T>();
            using (var cmd = new MySqlCommand(sql, _connection))
            {
                await stmtSetter(cmd);
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
