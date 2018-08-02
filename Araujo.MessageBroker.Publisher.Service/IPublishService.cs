using System;
using System.Collections.Generic;
using System.Text;

namespace Araujo.MessageBroker.Publisher.Service
{
    public interface IPublishService
    {
        void Publish(string messageContent);
    }
}
