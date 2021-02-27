using MessengerClient.DbModels;
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
        LocalUser localUser;

        public string Name 
        { 
            get => localUser.Name; 
        }

        public DateTime LastMessageTime
        {
            get => LocalMessageRepository.GetLastMessageByReceiver(localUser).SendedTime;
        }

        public string LocalLastMessageTime
        {
            get => LastMessageTime.ToLocalTime().ToString("H:mm dd, MMM");
        }

        public UserWrapper(LocalUser localUser)
        {
            this.localUser = localUser;
        }
    }
}
