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

        public static List<User> GetUsers()
        {
            List<LocalUser> localUsers = Connection.Table<LocalUser>().ToList();
            return localUsers.Select(u => new User() { Name = u.Name }).ToList();
        }
    }
}
