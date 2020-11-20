using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MessengerClient.Constant;
using System.Linq;
using MessengerClient.DbModels;
using System.Diagnostics;
using MessageCore.Models;

namespace MessengerClient.Datasource
{
    class SqliteDatasource
    {
        public static SQLiteConnection Connection { get; private set; }

        public SqliteDatasource()
        {
            if (Connection is null)
            {
                Connection = new SQLiteConnection(Constants.DatabasePath);

                Debug.WriteLine(Constants.DatabasePath);

                Connection.CreateTable<LocalUser>();
                Connection.CreateTable<LocalMessage>();
                Connection.CreateTable<LocalThisUser>();
            }
        }
    }
}
