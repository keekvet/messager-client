using MessageCore.Models;
using MessengerClient.Constant;
using MessengerClient.Models;
using MessengerClient.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MessengerClient.ViewModels
{
    class RegistrationViewModel : INotifyPropertyChanged
    {
        public Command Register { get; private set; }
        public Command LogIn { get; private set; }
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



        public RegistrationViewModel()
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

            Register = new Command(() =>
            {
                userHandler.SendRegister(new User() { Name = userNameMaybe, Password = userPasswordMaybe });
            });


            LogIn = new Command(() =>
            {
                userNameMaybe = string.Empty;
                userNameMaybe = string.Empty;
                App.Current.MainPage = new LogInPage();
            });
        }
    }
}
