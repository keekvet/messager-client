using MessengerClient.Constant;
using MessengerClient.Models;
using MessengerClient.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MessengerClient.ViewModels
{
    class UserCellViewModel
    {

        public void EditUser(DeleteConversationOptions options, string userName)
        {
            if(options.HasFlag(DeleteConversationOptions.DeleteMessages))
            {
                LocalMessageRepository.RemoveMessagesByUser(userName);
            }
            if (options.HasFlag(DeleteConversationOptions.DeleteUser))
            {
                LocalUserRepository.DeleteUser(userName, UserHandler.GetInstance().User);
            }
            MessagingCenter.Send(this, MessageKeys.CONVERSATION_CHANGED, userName);
        }
    }

    [Flags]
    enum DeleteConversationOptions
    {
        DeleteUser = 1,
        DeleteMessages = 2
    }
}
