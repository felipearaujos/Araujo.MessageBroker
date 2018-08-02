using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Araujo.MessageBroker.Rabbit
{
    public class RabbitMqHelper : IRabbitMQHelper
    {
        private IConnection connection { get; set; }

        private void OpenConnection()
        {
            if(connection == null)
                connection = this.CreateConnection(GetConnectionFactory());
        }
        

        private ConnectionFactory GetConnectionFactory()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "127.0.0.1",
                UserName = "guest",
                Password = "guest"
            };
            return connectionFactory;
        }

        public T RetrieveSingleMessage<T>(string queueName)
        {
            this.OpenConnection();

            BasicGetResult data;
            using (var channel = connection.CreateModel())
            {                
                data = channel.BasicGet(queueName, autoAck: false);
            }

            if (data != null)
                return JsonConvert.DeserializeObject<T>(System.Text.Encoding.UTF8.GetString(data.Body));

            return default(T);
        }

        public uint RetrieveMessageCount(string queueName)
        {
            this.OpenConnection();

            uint data;
            using (var channel = connection.CreateModel())
            {
                data = channel.MessageCount(queueName);
            }
            return data;
        }

        private IConnection CreateConnection(ConnectionFactory connectionFactory)
        {
            return connectionFactory.CreateConnection();
        }

        public QueueDeclareOk CreateQueue(string queueName)
        {
            this.OpenConnection();

            connection = this.CreateConnection(GetConnectionFactory());

            QueueDeclareOk queue;
            using (var channel = connection.CreateModel())
            {
                queue = channel.QueueDeclare(queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);                
            }
            return queue;
        }

        public List<T> RetrieveMessageList<T>(string queueName)
        {
            this.OpenConnection();
            int count = 0;
            var messageList = new List<T>();

            using (var channel = connection.CreateModel())
            {
                this.CreateQueue(queueName);

                var messageCount = channel.MessageCount(queueName);
                
                var consumer = new QueueingBasicConsumer(channel);
                
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);

                while (count < messageCount)
                {
                    var dequeue = consumer.Queue.Dequeue();

                    var body = dequeue.Body;
                    var message = Encoding.UTF8.GetString(body);

                    var result = JsonConvert.DeserializeObject<T>(message);

                    messageList.Add(result);
                    //channel.BasicAck(dequeue.DeliveryTag, false);
                    count++;
                }
            }

            return messageList;
        }

        public bool WriteMessageOnQueue<T>(T message, string queueName)
        {
            this.OpenConnection();

            this.CreateQueue(queueName);

            using (var channel = connection.CreateModel())
            {
                var serialized = JsonConvert.SerializeObject(message);

                channel.BasicPublish(string.Empty, queueName, null, Encoding.ASCII.GetBytes(serialized));
            }
            return true;
        }
    }
}