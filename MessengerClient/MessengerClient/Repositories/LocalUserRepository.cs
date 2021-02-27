using MessageCore.Models;
using MessengerClient.Datasource;
using MessengerClient.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengerClient.Repositories
{
    class LocalUserRepository : SqliteDatasource
    {
        public static void AddUser(User user)
        {
            Connection.Insert(new LocalUser(user));
        }

        public static List<LocalUser> GetLocalUsers()
        {
            return Connection.Table<LocalUser>().ToList();
        }

    }
}
