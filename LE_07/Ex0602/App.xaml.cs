using System;
using System.Windows;
using Ex0602.Services;
using Ex0602.View;
using DBLib;
using MySql.Data.MySqlClient;

namespace Ex0602
{

    public partial class App : Application
    {
        public MySqlConnection DbConnection { get; set; }
        public DBService DbService { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            InitializeDatabase();

            DbConnection = (MySqlConnection)DBConnection.GetConnection("data_manage_notes");
            DbConnection.Open();

            DbService = new DBService(DbConnection);

            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void InitializeDatabase()
        {
            try
            {
                DBInitializer.CreateDatabaseAndTables();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during batabase setup: " + ex.Message,
                    "Fatal Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
                return;
            }
        }
    }
}
