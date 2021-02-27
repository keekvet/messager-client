using MessageCore.Models;
using MessengerClient.Constant;
using MessengerClient.Models;
using MessengerClient.Views;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MessengerClient.ViewModels
{
    class LogInViewModel : INotifyPropertyChanged
    {
        public Command Login { get; private set; }
        public Command Registration { get; private set; }
        private static UserHandler userHandler = UserHandler.GetInstance();

        public event PropertyChangedEventHandler PropertyChanged;

        private static string userNameMaybe;
        public string UserNameMaybe
        {
            get => userNameMaybe;
            set
            {
                userNameMaybe = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserNameMaybe)));
            }
        }

        private static string userPasswordMaybe;
        public string UserPasswordMaybe
        {
            get => userPasswordMaybe;
            set
            {
                userPasswordMaybe = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserPasswordMaybe)));
            }
        }

        

        public LogInViewModel()
        {
            MessagingCenter.Subscribe<UserHandler, bool>(this, MessageKeys.USER_FOUND, (sender, arg) =>
            {
                if (arg)
                {
                    userHandler.User = new User() { Name = userNameMaybe, Password = userPasswordMaybe };
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        App.Current.MainPage = new MainPage();
                    });
                }
                userNameMaybe = string.Empty;
                userNameMaybe = string.Empty;
            });

            Login = new Command(() =>
            {
                userHandler.SendLogin(new User() { Name = userNameMaybe, Password = userPasswordMaybe });
            });


            Registration = new Command(() =>
            {
                userNameMaybe = string.Empty;
                userNameMaybe = string.Empty;
                App.Current.MainPage = new RegistrationPage();
            });
        }

    }
}
