using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using App.Shared;
using Microsoft.Extensions.Options;
using App.Application.Interfaces;
using App.Application.Services;
using System.Configuration;
using App.Infra.Data.Interfeces;
using App.Infra.Data.Repositories;
using App.Infra.Data.Interfaces;
using App.Infra.Data.Context;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace App.CrossCutting.IoC
{
    public static class NativeInjectionBootStrapper
    {
        public static IServiceCollection Configuration()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            var config = LoadConfiguration();

            //var rabbitMqConfig = new RabbitMQConfigurations();
            //new ConfigureFromConfigurationOptions<RabbitMQConfigurations>(
            //    config.GetSection("rabbitMQConfigurations")
            //).Configure(rabbitMqConfig);

            serviceCollection.AddSingleton(config);
            serviceCollection.Configure<RabbitMQConfigurations>(config.GetSection("rabbitMQConfigurations"));

            serviceCollection.AddSingleton<IBrokerService,BrokerService>();
            serviceCollection.AddSingleton<IWorkflowService, WorkflowService>();
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped<IBankAccountRepository, BankAccountRepository>();

            serviceCollection.AddDbContext<BanckContext>(options =>
                options.UseSqlServer("Data Source=LAPTOP-ANDREMAR;Integrated Security=True").UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                , ServiceLifetime.Transient
            );

            return serviceCollection;
        }

        public static IConfiguration LoadConfiguration()
        {
            var configuration =  new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            return configuration;
        }
    }
}
