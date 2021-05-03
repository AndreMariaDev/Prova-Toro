using App.Application.Interfaces;
using App.Application.Services;
using App.Infra.Data.Interfaces;
using App.Infra.Data.Interfeces;
using App.Infra.Data.Repositories;
using App.Infra.Data.Repository;
using Broker;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CrossCutting.IoC
{
    public static class NativeInjectionBootStrapper
    {
        public static void RegisterServices(IServiceCollection services) 
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAssetsRepository, AssetsRepository>();
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<IBankAccountHistoryRepository, BankAccountHistoryRepository>();
            services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
            services.AddScoped<IUserCredentialsRepository, UserCredentialsRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAssetsService, AssetsService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IBankAccountHistoryService, BankAccountHistoryService>();
            services.AddScoped<IPatrimonyService, PatrimonyService>();
            services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();
            services.AddScoped<IUserCredentialsService, UserCredentialsService>();

            services.AddSingleton<IBrokerService, BrokerService>();
        }
    }
}
