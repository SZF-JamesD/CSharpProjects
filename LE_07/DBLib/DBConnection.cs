using System;
using System.IO;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace DBLib
{
    public static class DBConnection
    {
        private static readonly string connectionString;
        static DBConnection()
        {
            try
            {
                string configPath = Environment.GetEnvironmentVariable("DB_CONFIG_PATH") ?? "../../../../dbconfig.json";

                if (!File.Exists(configPath))
                {
                    throw new FileNotFoundException("Configuration file not found: " + configPath);
                }

                string json = File.ReadAllText(configPath);
                var jObject = JObject.Parse(json);
                connectionString = jObject["dbConnectionString"]?.ToString();

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("The 'dbConnectionString' was not found in the config file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading config: " + ex.Message);
                throw;
            }
        }

        public static DbConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }

        public static DbConnection GetConnection(string database)
        {
            var builder = new MySqlConnectionStringBuilder(connectionString)
            {
                Database = database
            };
            return new MySqlConnection(builder.ConnectionString);
        }
    }
}
