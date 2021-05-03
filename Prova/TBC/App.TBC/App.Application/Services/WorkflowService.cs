using App.Application.Interfaces;
using App.Shared;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
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


        public WorkflowService(IOptions<RabbitMQConfigurations> rabbitMQConfigurations)
        {
            _rabbitMQConfigurations = rabbitMQConfigurations.Value;
        }
        #region  Methods
        public void Process(string message)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            try
            {
                var transactionHistory = !String.IsNullOrEmpty(message) ? JsonConvert.DeserializeObject<TransactionHistory>(message) : default(TransactionHistory);
                var _event = transactionHistory.Event;

                BrokerService.SendQueue<TransactionHistory>(transactionHistory, "Bank Transaction", _rabbitMQConfigurations);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stopwatch.Stop();
            }
        }
        #endregion
    }
}
