//based on : https://github.com/thiagoloureiro/RabbitMQDotnet/blob/master/RabbitMQDotnet/RabbitMQHelper/IRabbitMQHelper.cs
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Araujo.MessageBroker.Rabbit
{
    public interface IRabbitMQHelper
    {
        T RetrieveSingleMessage<T>(string queueName);

        uint RetrieveMessageCount(string queueName);
               
        List<T> RetrieveMessageList<T>(string queueName);

        bool WriteMessageOnQueue<T>(T message, string queueName);
    }
}
