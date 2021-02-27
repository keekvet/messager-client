using MessageCore.Models;
using MessageRequest;
using MessengerClient.Constant;
using MessengerClient.DbModels;
using MessengerClient.Models;
using MessengerClient.Repositories;
using MessengerClient.Wrappers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MessengerClient.ViewModels
{

    class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private static UserHandler userHandler = UserHandler.GetInstance();
        private static MessageHandler messageHandler = MessageHandler.GetInstance();

        public Command AddUserCommand { get; private set; }
        public static ObservableCollection<UserWrapper> SavedUsers { get; private set; }

        private static string userToFind;
        public string UserToFind
        {
            get => userToFind;
            set
            {
                userToFind = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserViewModel)));
            }
        }

        public UserViewModel()
        {
            SavedUsers = new ObservableCollection<UserWrapper>();
            UpdateUsers();

            MessagingCenter.Subscribe<UserHandler, bool>(this, MessageKeys.RECEIVER_FOUND, (sender, arg) =>
            {
                if (arg)
                {
                    User foundReceiver = new User() { Name = userToFind };
                    if (!SavedUsers.Where(u => u.Name.Equals(foundReceiver.Name)).Any())
                    {
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            LocalUserRepository.AddUser(foundReceiver);
                        });
                        UpdateUsers();
                    }
                    userHandler.ReceiverFound = false;
                }

            });

            MessagingCenter.Subscribe<MessageHandler, bool>(this, MessageKeys.UPDATE_USER_LAST_MESSAGE, (sender, arg) =>
            {
                UpdateUsers();
            });

            AddUserCommand = new Command(() =>
            {
                userHandler.SendUserExist(userToFind);
            });
        }

        public void UpdateUsers()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SavedUsers.Clear();

                List<UserWrapper> sortedUsers = LocalUserRepository.GetLocalUsers().Select(u => new UserWrapper(u.User))
                    .OrderByDescending(u => u.LastMessageTime).ToList();

                foreach (UserWrapper user in sortedUsers)
                    SavedUsers.Add(user);
            });
        }

        public void SaveReceiver(User user)
        {
            userHandler.Receiver = user;
        }

    }
}
