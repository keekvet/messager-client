using MessageCore.Models;
using MessengerClient.Constant;
using MessengerClient.DbModels;
using MessengerClient.Models;
using MessengerClient.Repositories;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MessengerClient.ViewModels
{
    public class MessageViewModel : INotifyPropertyChanged
    {
        private MessageHandler messageHandler = MessageHandler.GetInstance();
        private UserHandler userHandler = UserHandler.GetInstance();

        public Command SendMessageCmd { get; set; }
        public ObservableCollection<LocalMessage> MessagesObservable { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public bool EditTextEnabled
        {
            get => !receiverName.Equals(Constants.DEFAULT_RECEIVER_TEXT);
        }

        private string receiverName = Constants.DEFAULT_RECEIVER_TEXT;
        public string ReceiverName
        {
            get => receiverName;
            set
            {
                receiverName = value;

                MessagesObservable.Clear();

                if (!value.Equals(Constants.DEFAULT_RECEIVER_TEXT))
                    foreach (LocalMessage message in
                    messageHandler.GetMessagesWithUsers(userHandler.User.Name, receiverName))
                    {
                        HandleAndShow(message);
                    }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditTextEnabled)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ReceiverName)));
            }
        }

        private string wroteMessageText;
        public string WroteMessageText
        {
            get => wroteMessageText;
            set
            {
                wroteMessageText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WroteMessageText)));
            }
        }

        private void HandleAndShow(LocalMessage message)
        {
            if (message.Receiver == receiverName)
            {
                message.HorizontalOptionText = LayoutOptions.End;
                message.HorizontalOptionTime = LayoutOptions.End;
            }
            else
            {
                message.HorizontalOptionText = LayoutOptions.Start;
                message.HorizontalOptionTime = LayoutOptions.Start;
            }
            MessagesObservable.Add(message);
        }

        public void SendMessage()
        {
            if (WroteMessageText is null)
                return;

            WroteMessageText = WroteMessageText.Trim();

            if (WroteMessageText.Equals(string.Empty))
                return;

            LocalMessage message = new LocalMessage()
            {
                Receiver = userHandler.Receiver.Name,
                Sender = userHandler.User.Name,
                Text = WroteMessageText
            };
            messageHandler.SaveAndShowMessage(message);
            messageHandler.SendMessage(message);
            WroteMessageText = string.Empty;
        }


        public MessageViewModel()
        {
            messageHandler.SendGetAllMessages();

            MessagesObservable = new ObservableCollection<LocalMessage>();

            MessagingCenter.Subscribe<UserHandler, User>(this, MessageKeys.RECEIVER_TRANSFER, (sender, arg) =>
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    if (arg is null || arg.Name.Equals(string.Empty))
                        ReceiverName = Constants.DEFAULT_RECEIVER_TEXT;
                    else
                        ReceiverName = arg.Name;
                });
            });

            MessagingCenter.Subscribe<MessageHandler, bool>(this, MessageKeys.MESSAGE_AVAILABLE, (sender, arg) =>
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    while (!messageHandler.NewMessages.IsEmpty)
                    {
                        LocalMessage message;
                        messageHandler.NewMessages.TryTake(out message);
                        HandleAndShow(message);
                    }
                });
            });

            MessagingCenter.Subscribe<MessageHandler, LocalMessage>(this, MessageKeys.MESSAGE_HIDE, (sender, arg) =>
            {
                MainThread.InvokeOnMainThreadAsync(() =>
                {
                    MessagesObservable.Remove(arg);
                });
            });


            MessagingCenter.Subscribe<UserCellViewModel, string>(this, MessageKeys.CONVERSATION_CHANGED, (sender, arg) =>
            {
                userHandler.Receiver = null;
            });

            SendMessageCmd = new Command(SendMessage);
        }
    }
}
