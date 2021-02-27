using MessengerClient.DbModels;
using MessengerClient.PlatformDepended;
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
        private MessageViewModel messageViewModel;

        public MessagePage()
        {
            InitializeComponent();

            messageViewModel = new MessageViewModel();

            send.Command = messageViewModel.SendMessageCmd;

            messageViewModel.MessagesObservable.CollectionChanged += (sender, args) =>
            {
                if (scrollDownSwitch.IsToggled)
                    messagesList.ScrollTo(
                            messagesList.ItemsSource.Cast<object>().LastOrDefault(),
                            ScrollToPosition.Start,
                            false);
            };

            messagesList.ItemsSource = messageViewModel.MessagesObservable;
            messagesList.ItemTemplate = new DataTemplate(typeof(MessageCell));

            //add shortcut for Windows
            KeyboardHandlerParent keyboardHandler = DependencyService.Get<KeyboardHandlerParent>();
            keyboardHandler.RegisterShortcut(messageViewModel);


            receiverName.SetBinding(Label.TextProperty,
                new Binding("ReceiverName", source: messageViewModel));
            messageEditor.SetBinding(Editor.TextProperty,
                new Binding("WroteMessageText", source: messageViewModel, mode: BindingMode.TwoWay));
            messageEditor.SetBinding(Editor.IsVisibleProperty,
                new Binding("EditTextEnabled", source: messageViewModel));
            messageEditor.SetBinding(Editor.IsFocusedProperty,
                new Binding("ShortcutListenerActive", source: keyboardHandler));
            send.SetBinding(Button.IsVisibleProperty,
                new Binding("EditTextEnabled", source: messageViewModel));
        }

    }
}