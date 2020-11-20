using MessageCore.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerClient.DbModels
{
    [Table("local_user")]
    class LocalUser
    {
        [Column("name")]
        [PrimaryKey]
        public string Name { get; set; }

        public LocalUser(){}

        public LocalUser(User user)
        {
            Name = user.Name;
        }
    }
}
