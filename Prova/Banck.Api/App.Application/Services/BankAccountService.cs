using App.Application.Interfaces; 
using App.Domain.Models; 
using App.Infra.Data.Interfaces;
using App.Domain.Models;
using App.Infra.Data.Interfeces;
using App.Shared.Extensions;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Broker;
using Microsoft.Extensions.Options;

namespace App.Application.Services 
{
    public class BankAccountService : BaseService<BankAccount>, IBankAccountService
    {
        private readonly IBankAccountRepository _BankAccountRepository;
        private readonly IBrokerService _BrokerService;
        private readonly RabbitMQConfigurations _rabbitMQConfigurations;

        public BankAccountService(IBankAccountRepository BankAccountRepository, IBrokerService BrokerService, IOptions<RabbitMQConfigurations> rabbitMQConfigurations) : base(BankAccountRepository)
        {
            _BankAccountRepository = BankAccountRepository;
            _BrokerService = BrokerService;
            _rabbitMQConfigurations = rabbitMQConfigurations.Value;
        }
        public async Task<BankBalance> GetAmountInAccount(Guid codeUser)
        {
            return await this._BankAccountRepository.GetAmountInAccount(codeUser);
        }
        public async Task<Boolean> GetExistBankAccount(String accountNumber, String branch)
        {
            return await this._BankAccountRepository.GetExistBankAccount(accountNumber, branch);
        }
        public async Task<List<BankAccount>> GetBankAccountByUser(Guid codeUser)
        {
            return await this._BankAccountRepository.GetBankAccountByUser(codeUser);
        }

        public async Task<bool> RunTransaction(TransactionHistoryResponse entity)
        {
            if (entity.OriginBranch == String.Format("{0}/ {1}", entity.TargetBranch, entity.TargetAccount))
            {
                TransactionHistory itemTransaction = new TransactionHistory()
                {
                    Event = entity.Event,
                    TargetBank = entity.TargetBank,
                    TargetBranch = entity.TargetBranch,
                    TargetAccount = entity.TargetAccount,
                    OriginBank = entity.OriginBank,
                    OriginBranch = entity.OriginBranch,
                    OriginDocument = entity.OriginDocument,
                    Amount = entity.Amount
                };
                _BrokerService.SendQueue<TransactionHistory>(itemTransaction, "Bank Transaction BC", _rabbitMQConfigurations);
                return true;
            }
            else
            {
                var resultRun = await _BankAccountRepository.RunTransaction(entity);
                if (resultRun)
                {
                    TransactionHistory itemTransaction = new TransactionHistory()
                    {
                        Event = entity.Event,
                        TargetBank = entity.TargetBank,
                        TargetBranch = entity.TargetBranch,
                        TargetAccount = entity.TargetAccount,
                        OriginBank = entity.OriginBank,
                        OriginBranch = entity.OriginBranch,
                        OriginDocument = entity.OriginDocument,
                        Amount = entity.Amount
                    };
                    _BrokerService.SendQueue<TransactionHistory>(itemTransaction, "Bank Transaction BC", _rabbitMQConfigurations);
                    return true;
                }
            }
            return false;
        }
    } 
} 
