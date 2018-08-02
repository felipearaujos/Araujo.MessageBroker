using System;
using System.Collections.Generic;
using System.Text;
using Araujo.MessageBroker.Rabbit;

namespace Araujo.MessageBroker.Consumer.Service
{
    public class ConsumerService : IConsumerService
    {
        private IRabbitMQHelper mq { get; set; }

        public ConsumerService(IRabbitMQHelper mq)
        {
            this.mq = mq;
        }

        public List<Message> ListMessages()
        {
            List<Message> messages = mq.RetrieveMessageList<Message>(new Message().GetType().ToString());

            return messages;
        }
    }
}
