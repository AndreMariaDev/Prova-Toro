using App.Shared.Extensions;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broker
{
    public class BrokerService: IBrokerService
    {
        public void SendQueue<T>(T entity, string queueName, RabbitMQConfigurations rabbitMQConfigurations)
        {
            var factory = new ConnectionFactory
            {
                HostName = rabbitMQConfigurations.HostName,
                Port = Convert.ToInt32(rabbitMQConfigurations.Port),
                UserName = rabbitMQConfigurations.UserName,
                Password = rabbitMQConfigurations.Password
            };

            try
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(entity));

                    channel.BasicPublish(exchange: "",
                                         routingKey: queueName,
                                         basicProperties: null,
                                         body: body);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
