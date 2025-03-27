using System;
using MySql.Data.MySqlClient;
using DBLib;

namespace Ex0602.Services
{
    public class DBInitializer
    {
        public static void CreateDatabaseAndTables()
        {
            try
            {
                using (var connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    string createDatabaseQuery = "create database if not exists data_manage_notes;";

                    using (var cmd = connection.CreateCommand())
                    {
                        cmd.CommandText = createDatabaseQuery;
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Database created or already exists.");
                    }
                }

                using (var dbConnection = DBConnection.GetConnection("data_manage_notes"))
                {
                    dbConnection.Open();

                    string createEmployeesTable = @"
                        create table if not exists employees(
                            employee_id int primary key auto_increment,
                            employee_last_name varchar(50)
                            );";

                    using (var cmd = dbConnection.CreateCommand())
                    {
                        cmd.CommandText = createEmployeesTable;
                        cmd.ExecuteNonQuery();
                    }

                    string createNotesTable = @"
                        create table if not exists notes(
                            note_id int primary key auto_increment,
                            content varchar(255) not null,
                            date_created datetime not null default current_timestamp,
                            employee_id int not null,
                            foreign key (employee_id) references employees(employee_id)
                            );";

                    using (var cmd = dbConnection.CreateCommand())
                    {
                        cmd.CommandText = createNotesTable;
                        cmd.ExecuteNonQuery();
                    }

                    Console.WriteLine("Tables created or already exist.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error creating databse or tables: " + ex.Message);
                throw;
            }
        }
    }
}
