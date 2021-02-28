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
        public static void AddUser(User user, User contactOwner)
        {
            Connection.Insert(new LocalUser(user, contactOwner));
        }

        public static List<LocalUser> GetLocalUsers()
        {
            return Connection.Table<LocalUser>().ToList();
        }

        public static void DeleteUser(string userName, User contactOwner)
        {
            LocalUser user = Connection.Table<LocalUser>()
                .Where(u => u.Name.Equals(userName) && u.ContactOwner.Equals(contactOwner.Name)).FirstOrDefault();
            if(!(user is null))
                Connection.Delete(user);
        }

    }
}
