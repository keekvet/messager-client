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
    public partial class RegistrationPage : ContentPage
    {
        RegistrationViewModel registrationViewModel;

        public RegistrationPage()
        {
            InitializeComponent();
            registrationViewModel = new RegistrationViewModel();

            nickname.SetBinding(Entry.TextProperty,
                new Binding(path: "UserNameMaybe", source: registrationViewModel, mode: BindingMode.OneWayToSource));
            password.SetBinding(Entry.TextProperty,
                new Binding(path: "UserPasswordMaybe", source: registrationViewModel, mode: BindingMode.OneWayToSource));

            submit.Command = registrationViewModel.Register;
            logIn.Command = registrationViewModel.LogIn;
        }
    }
}