using App.Application.Interfaces;
using App.Infra.Data.Interfaces;
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
            try
            {
                var transactionHistory = !String.IsNullOrEmpty(message) ? JsonConvert.DeserializeObject<TransactionHistory>(message) : default(TransactionHistory);
                _bankAccountRepository.RunTransaction(transactionHistory).Wait();
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
