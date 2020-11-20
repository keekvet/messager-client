using MessengerClient.Models;
using MessengerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MessengerClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogInPage : ContentPage
    {
        LogInViewModel logInViewModel;

        public LogInPage()
        {
            InitializeComponent();
            logInViewModel = new LogInViewModel();

            nickname.SetBinding(Entry.TextProperty, 
                new Binding(path: "UserNameMaybe", source: logInViewModel, mode: BindingMode.OneWayToSource));
            password.SetBinding(Entry.TextProperty, 
                new Binding(path: "UserPasswordMaybe", source: logInViewModel, mode: BindingMode.OneWayToSource));

            submit.Command = logInViewModel.Login;
            registration.Command = logInViewModel.Registration;
        }
    }
}