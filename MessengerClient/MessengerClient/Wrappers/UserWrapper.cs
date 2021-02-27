using MessageCore.Models;
using MessengerClient.Constant;
using MessengerClient.DbModels;
using MessengerClient.Models;
using MessengerClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient.Wrappers
{
    class UserWrapper
    {
        UserHandler userHandler = UserHandler.GetInstance();

        private User user;

        public string Name
        {
            get => user.Name;
        }

        public DateTime LastMessageTime
        {
            get => LocalMessageRepository.GetLastMessageWithUsers(user.Name, userHandler.User.Name).SendedTime;
        }

        public string LocalLastMessageTime
        {
            get
            {
                if (LastMessageTime.Equals(DateTime.MinValue))
                    return "no messages yet";
                else
                    return LastMessageTime.ToLocalTime().ToString(Constants.MESSAGE_DATE_FORMAT);
            }
        }

        public UserWrapper(User localUser)
        {
            this.user = localUser;
        }

        public User User { get => user; }
    }
}
