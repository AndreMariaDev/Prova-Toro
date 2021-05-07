using App.Application.Interfaces;
using App.Infra.Data.Interfaces;
using App.Shared;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class WorkflowService : IWorkflowService
    {
        private readonly RabbitMQConfigurations _rabbitMQConfigurations;
        private readonly IBankAccountRepository _bankAccountRepository;
        #region  Methods

        public WorkflowService(IOptions<RabbitMQConfigurations> rabbitMQConfigurations, IBankAccountRepository bankAccountRepository)
        {
            _rabbitMQConfigurations = rabbitMQConfigurations.Value;
            _bankAccountRepository = bankAccountRepository;
        }
        public void Process(string message)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            bool result = false;
            try
            {
                var transactionHistory = !String.IsNullOrEmpty(message) ? JsonConvert.DeserializeObject<TransactionHistory>(message) : default(TransactionHistory);
                result = _bankAccountRepository.RunTransaction(transactionHistory).Result;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
            }
            finally
            {
                if (!result) {
                    var entity = !String.IsNullOrEmpty(message) ? JsonConvert.DeserializeObject<TransactionHistory>(message) : default(TransactionHistory);
                    SendQueue<TransactionHistory>(entity, "Bank Transaction Error", this._rabbitMQConfigurations);
                }
                stopwatch.Stop();
            }
        }
        #endregion

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
