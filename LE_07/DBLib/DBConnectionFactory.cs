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
        public static Func<DbConnection> CreateConnection(string dbName = null)
        {
            return () =>
            {
                if (string.IsNullOrEmpty(dbName))
                    return DBConnection.GetConnection();
                else
                    return DBConnection.GetConnection(dbName);
            };
        }
    }
}
