using MessageCore.Models;
using MessageRequest;
using MessengerClient.Constant;
using MessengerClient.Models;
using MessengerClient.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public static ObservableCollection<User> SavedUsers { get; private set; }

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
            SavedUsers = new ObservableCollection<User>(LocalUserRepository.GetUsers());

            MessagingCenter.Subscribe<UserHandler, bool>(this, MessageKeys.RECEIVER_FOUND, (sender, arg) =>
            {
                if (arg)
                {
                    User foundReceiver = new User() { Name = userToFind };
                    if (!SavedUsers.Contains(foundReceiver))
                        MainThread.BeginInvokeOnMainThread(() =>
                        {
                            LocalUserRepository.AddUser(foundReceiver);
                            SavedUsers.Add(foundReceiver);
                        });
                    userHandler.ReceiverFound = false;
                }
            });

            AddUserCommand = new Command(() =>
            {
                userHandler.SendUserExist(userToFind);
            });
        }

        public void SaveReceiver(User user)
        {
            userHandler.Receiver = user;
        }

    }
}
