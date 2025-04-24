using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLib
{
    public static class DbConnectionFactory
    {
        public static Func<DbConnection> CreateConnection(string configPath, string dbName = null)
        {
            DBConnection.LoadConnection(configPath);

            return () =>
            {
                return string.IsNullOrEmpty(dbName)
                    ? DBConnection.GetConnection()
                    : DBConnection.GetConnection(dbName);
            };
        }
    }
}
