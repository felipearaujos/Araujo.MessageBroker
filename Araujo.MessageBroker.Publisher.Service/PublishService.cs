using Araujo.MessageBroker.Rabbit;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Sockets;

namespace Araujo.MessageBroker.Publisher.Service
{
    public class PublishService : IPublishService
    {
        private IRabbitMQHelper mq { get; set; }

        public PublishService(IRabbitMQHelper mq)
        {
            this.mq = mq;
        }

        public void Publish(string messageContent)
        {
            DateTime requestedDate = DateTime.Now;
            string sender = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    sender = ip.ToString();
                }
            }

            Message message = new Message(messageContent, requestedDate, sender);
            
            //mq.CreateQueue(message.GetType().ToString() );

            mq.WriteMessageOnQueue<Message>(message, message.GetType().ToString());



        }
    }
}
