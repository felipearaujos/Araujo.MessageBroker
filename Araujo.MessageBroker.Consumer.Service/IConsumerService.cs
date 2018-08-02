using Araujo.MessageBroker.Rabbit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Araujo.MessageBroker.Consumer.Service
{
    public interface IConsumerService
    {
        List<Message> ListMessages();

    }
}
