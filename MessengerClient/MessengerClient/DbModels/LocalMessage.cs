using MessageCore.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace MessengerClient.DbModels
{
    [Table("local_message")]
    public class LocalMessage : IComparable
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("local_id")]
        public int LocalId { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("sended_time")]
        public DateTime SendedTime { get; set; }

        [Column("receiver")]
        public string Receiver { get; set; }

        [Column("sender")]
        public string Sender { get; set; }

        [Ignore]
        public string SendedTimeLocal
        {
            get { return SendedTime.ToLocalTime().ToString("d MMM, HH:mm"); }
        }


        [Ignore]
        public LayoutOptions HorizontalOptionText { get; set; }

        [Ignore]
        public LayoutOptions HorizontalOptionTime { get; set; }

        public LocalMessage() { }

        public LocalMessage(Message message)   
        {
            Id = message.Id;
            LocalId = message.SenderLocalId;
            Text = message.Text;
            SendedTime = message.SendedTime;
            Receiver = message.Receiver;
            Sender = message.Sender;
        }

        public LocalMessage(string text, DateTime sendedTime, string receiver, string sender)
        {
            Text = text;
            SendedTime = sendedTime;
            Receiver = receiver;
            Sender = sender;
        }

        public Message GetMessage()
        {
            return new Message()
            {
                Id = Id,
                SenderLocalId = LocalId,
                Sender = Sender,
                Receiver = Receiver,
                SendedTime = SendedTime,
                Text = Text,
                State = MessageState.NoOneReceived
            };
        }

        public override bool Equals(object obj)
        {
            return obj is LocalMessage message &&
                   Id == message.Id &&
                   LocalId == message.LocalId &&
                   Text == message.Text &&
                   SendedTime == message.SendedTime &&
                   Receiver == message.Receiver &&
                   Sender == message.Sender;
        }

        public int CompareTo(object obj)
        {
            return SendedTime.CompareTo(obj);
        }
    }
}