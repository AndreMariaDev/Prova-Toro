using App.Application.Interfaces;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Shared;
using RabbitMQ.Client.Events;
using System.Threading;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;

namespace App.Application.Services
{
    public class BrokerService : IBrokerService
    {
        private static readonly AutoResetEvent WaitHandle = new AutoResetEvent(false);
        private readonly RabbitMQConfigurations _rabbitMQConfigurations ;
        public readonly IWorkflowService _workflowService;
        public BrokerService(IOptions<RabbitMQConfigurations> rabbitMQConfigurations, IWorkflowService workflowService ) {
            _rabbitMQConfigurations = rabbitMQConfigurations.Value;
            _workflowService = workflowService;
        }
        public void StartBroker()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _rabbitMQConfigurations.HostName,
                    Port = Convert.ToInt32(_rabbitMQConfigurations.Port),
                    UserName = _rabbitMQConfigurations.UserName,
                    Password = _rabbitMQConfigurations.Password
                };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        "Bank Transaction",
                        true,
                        false,
                        false,
                        null
                    );
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (sender, args) => ProcessMessage.ConsumerReceived
                    (
                        sender
                        , args
                        , _workflowService
                        , channel
                    );
                    channel.BasicConsume(
                        "Bank Transaction",
                        true,
                        consumer
                    );

                    Console.WriteLine("Waiting messages to processing");
                    Console.WriteLine("Enter any key to close...");

                    // Tratando o encerramento da aplicação com
                    // Control + C ou Control + Break
                    Console.CancelKeyPress += (o, e) =>
                    {
                        Console.WriteLine("Saindo...");

                        // Libera a continuação da thread principal
                        WaitHandle.Set();
                    };

                    // Aguarda que o evento CancelKeyPress ocorra
                    WaitHandle.WaitOne();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static void SendQueue<T>(T entity, string queueName, RabbitMQConfigurations rabbitMQConfigurations)
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
