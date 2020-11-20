using MessengerClient.DbModels;
using MessengerClient.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Markup;
using Xamarin.Forms.Xaml;

namespace MessengerClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {
        private const int MESSAGE_EDITOR_MAX_HEIGHT = 200;
        private MessageViewModel messageViewModel;

        public MessagePage()
        {
            InitializeComponent();
            MessageEditorResize();

            messageViewModel = new MessageViewModel();

            send.Command = messageViewModel.SendMessage;

            messagesList.ItemsSource = messageViewModel.MessagesObservable;
            messagesList.ItemTemplate = new DataTemplate(typeof(MessageCell));

            messageViewModel.MessagesObservable.CollectionChanged += (sender, args) =>
            {
                if (scrollDownSwitch.IsToggled)
                    messagesList.ScrollTo(
                            messagesList.ItemsSource.Cast<object>().LastOrDefault(),
                            ScrollToPosition.Start,
                            false);
            };

            receiverName.SetBinding(Label.TextProperty,
                new Binding("ReceiverName", source: messageViewModel));
            messageEditor.SetBinding(Editor.TextProperty,
                new Binding("WroteMessageText", source: messageViewModel, mode: BindingMode.TwoWay));
            messageEditor.SetBinding(Editor.IsVisibleProperty,
                new Binding("EditTextEnabled", source: messageViewModel));
            send.SetBinding(Button.IsVisibleProperty,
                new Binding("EditTextEnabled", source: messageViewModel));

        }

        private void MessageEditorResize()
        {
            messageEditor.TextChanged += (sender, e) =>
            {
                if (messageEditor.Height > MESSAGE_EDITOR_MAX_HEIGHT)
                {
                    messageEditor.HeightRequest = MESSAGE_EDITOR_MAX_HEIGHT;
                }
                else if (messageEditor.Height == MESSAGE_EDITOR_MAX_HEIGHT)
                {
                    messageEditor.HeightRequest = -1;
                }
            };

            messageEditor.SizeChanged += (sender, e) =>
            {
                if (messageEditor.Height > MESSAGE_EDITOR_MAX_HEIGHT)
                {
                    messageEditor.HeightRequest = MESSAGE_EDITOR_MAX_HEIGHT;
                }
            };
        }
    }
}