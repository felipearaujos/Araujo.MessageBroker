using System;
using System.Collections.Generic;
using System.Text;

namespace Araujo.MessageBroker.Rabbit
{
    public class Message
    {
        public string Content { get; private set; }
        public DateTime RequestedDate { get; private set; }
        public bool  Published { get; private set; }
        public string  Sender { get; set; }
        public Message(string content, DateTime requestedDate, string sender)
        {
            this.Content = content;
            this.RequestedDate = requestedDate;
            this.Sender = sender;
        }

        public Message()
        {

        }
    }
}
