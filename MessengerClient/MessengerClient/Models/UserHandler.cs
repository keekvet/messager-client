using MessageCore.Models;
using MessageRequest;
using MessengerClient.Constant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace MessengerClient.Models
{
    class UserHandler : INotifyPropertyChanged
    {
        private static UserHandler userModel;


        public event PropertyChangedEventHandler PropertyChanged;

        private User thisUser;
        public User User
        {
            get => thisUser;
            set
            {
                if (thisUser is null && !(value is null))
                {
                    thisUser = value;
                }
            }
        }

        private User receiver;
        public User Receiver
        {
            get => receiver;
            set
            {
                receiver = value;
                MessagingCenter.Send(this, MessageKeys.RECEIVER_TRANSFER, value);
            }
        }

        private bool receiverFound;
        public bool ReceiverFound
        {
            get => receiverFound;
            set
            {
                receiverFound = value;
                MessagingCenter.Send(this, MessageKeys.RECEIVER_FOUND, value);
            }
        }

        private bool found;
        public bool Found
        {
            get => found;
            set
            {
                found = value;
                MessagingCenter.Send(this, MessageKeys.USER_FOUND, value);
            }
        }

        private UserHandler() { }

        public static UserHandler GetInstance()
        {
            if (userModel is null)
                userModel = new UserHandler();
            return userModel;
        }

        public void SendUserExist(string userToFind)
        {
            ServerConnectionHandler.RequestsToSend.Add(RequestConverter.ComposeUserExist(userToFind));
        }

        internal void SendRegister(User user)
        {
            ServerConnectionHandler.RequestsToSend.Add(
                 RequestConverter.ComposeRegistration(user));
        }

        public void SendLogin(User user)
        {
            ServerConnectionHandler.RequestsToSend.Add(
                RequestConverter.ComposeLogin(user));
        }

    }
}
