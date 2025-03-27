using System;
using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DBLib
{
    public static class DBInitializer
    {
        public static void CreateDatabaseAndTables(string databaseName, string CreateTablesSql)
        {
            try
            {
                using (var connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    var createDatabaseQuery = $"CREATE DATABASE IF NOT EXISTS '{databaseName}';";

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = createDatabaseQuery;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine($"Database '{databaseName}' created or already exists.");
                    }
                }

                using (var dbConnection = DBConnection.GetConnection(databaseName))
                {
                    dbConnection.Open();

                    using (var cmd = dbConnection.CreateCommand())
                    {
                        cmd.CommandText = CreateTablesSql;
                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("Tables created or already exist");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating database or tables: {ex.Message}");
                throw;
            }
        }
    }
}
