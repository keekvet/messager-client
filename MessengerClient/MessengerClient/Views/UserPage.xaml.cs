using MessageCore.Models;
using MessengerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace MessengerClient.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private UserViewModel userViewModel;

        public UserPage()
        {
            InitializeComponent();
            userViewModel = new UserViewModel();

            userList.ItemsSource = UserViewModel.SavedUsers;

            userList.ItemTemplate = new DataTemplate(typeof(UserCell));

            userList.ItemSelected += (sender, args) =>
            {
                userViewModel.SaveReceiver(userList.SelectedItem as User);
                TabbedPage parentPage = App.Current.MainPage as TabbedPage;
                parentPage.CurrentPage = parentPage.Children[1];
            };

            addUser.Command = userViewModel.AddUserCommand;
            userToFind.SetBinding(Entry.TextProperty, 
                new Binding(path: "UserToFind", source: userViewModel, mode: BindingMode.TwoWay));
        }
    }
}