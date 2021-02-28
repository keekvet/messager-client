using MessengerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Core;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MessengerClient.Constant;
using MessengerClient.Wrappers;

namespace MessengerClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserCell : ViewCell
    {
        private UserCellViewModel userCellViewModel;
        public UserCell()
        {
            InitializeComponent();

            userCellViewModel = new UserCellViewModel();

            delete.Clicked += async (sender, args) =>
            {
                if (!delete.IsFocused)
                    delete.BackgroundColor = Color.Red;
                else
                {
                    DeleteConversationOptions options;

                    string action = await App.Current.MainPage.DisplayActionSheet(
                        "What do with conversation?", 
                        "Cancel", 
                        null, 
                        Constants.RM_CONVER, 
                        Constants.DEL_MESS, 
                        Constants.DEL_CONVER);

                    switch (action)
                    {
                        case Constants.RM_CONVER:
                            options = DeleteConversationOptions.DeleteUser;
                            break;
                        case Constants.DEL_MESS:
                            options = DeleteConversationOptions.DeleteMessages;
                            break;
                        case Constants.DEL_CONVER:
                            options = DeleteConversationOptions.DeleteUser | DeleteConversationOptions.DeleteMessages;
                            break;
                        default:
                            return;
                    }
                    userCellViewModel.EditUser(options, (this.View.BindingContext as UserWrapper).Name);
                }
            };
        }
    }
}