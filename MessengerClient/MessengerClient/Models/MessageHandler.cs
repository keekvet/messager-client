using MessageCore.DTO;
using MessageCore.Models;
using MessageRequest;
using MessengerClient.Constant;
using MessengerClient.Datasource;
using MessengerClient.DbModels;
using MessengerClient.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MessengerClient.Models
{
    class MessageHandler : INotifyPropertyChanged
    {
        UserHandler userHandler = UserHandler.GetInstance();

        private static MessageHandler messageModel;

        public event PropertyChangedEventHandler PropertyChanged;
        public ConcurrentBag<LocalMessage> NewMessages { get; }

        private MessageHandler()
        {
            NewMessages = new ConcurrentBag<LocalMessage>();
        }

        public static MessageHandler GetInstance()
        {
            if (messageModel is null)
                messageModel = new MessageHandler();
            return messageModel;
        }

        public void SaveAndShowMessage(LocalMessage message)
        {
            LocalMessageRepository.AddMessage(message);
            RequestToShowMessage(message);
        }

        public IEnumerable<LocalMessage> GetMessagesWithUsers(string userName1, string userName2)
        {
            return LocalMessageRepository.GetMessagesWithUsers(userName1, userName2);
        }


        public void RequestToShowMessage(LocalMessage message)
        {
            if (userHandler.Receiver?.Name == message.Sender || userHandler.Receiver?.Name == message.Receiver)
            {
                NewMessages.Add(message);
                MessagingCenter.Send(this, MessageKeys.MESSAGE_AVAILABLE, true);
                MessagingCenter.Send(this, MessageKeys.UPDATE_USER_LAST_MESSAGE, true);
            }
        }

        public void RequestToHideMessage(LocalMessage message)
        {
            MessagingCenter.Send(this, MessageKeys.MESSAGE_HIDE, message);
        }

        public void SendMessage(LocalMessage message)
        {
            ServerConnectionHandler.RequestsToSend.Add(RequestConverter.ComposeMessage(message.GetMessage()));
        }

        public void SendGetAllMessages()
        {
            ServerConnectionHandler.RequestsToSend.Add(RequestConverter.ComposeGetAllMessages());
        }

        public void SyncMessage(MessageSyncInfo messageSyncInfo)
        {
            LocalMessage message = LocalMessageRepository.GetMessageByLocalId(messageSyncInfo.LocalId);
            if (message is null)
                return;

            message.Id = messageSyncInfo.Id;
            message.SendedTime = messageSyncInfo.NewDateTime;
            SaveAndShowMessage(message);

            RequestToHideMessage(LocalMessageRepository.GetMessageByLocalId(message.LocalId));
            LocalMessageRepository.RemoveMessageByLocalIdIfWithoutId(message.LocalId);
            
            ServerConnectionHandler.RequestsToSend.Add(
                RequestConverter.ComposeMessageSynchronised(message.Id));
        }
    }

}
