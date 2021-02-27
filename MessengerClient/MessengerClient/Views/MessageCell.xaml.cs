using MessengerClient.Constant;
using MessengerClient.DbModels;
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
    public partial class MessageCell : ViewCell
    {
        public MessageCell()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            if (text.HorizontalOptions.Equals(LayoutOptions.End))
                cell.BackgroundColor = Color.FromHex(Constants.SENDED_MESSAGE_COLOR);
            else
                cell.BackgroundColor = Color.FromHex(Constants.RECEIVED_MESSAGE_COLOR);
        }

    }
}