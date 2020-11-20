using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessengerClient.DbModels
{
    [Table("this_user")]
    class LocalThisUser
    {

        [Column("user_name")]
        public string LocalUser { get; set; }

        [Column("password")]
        public string Password { get; set; }
    }
}
