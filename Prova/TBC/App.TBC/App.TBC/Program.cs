using System;
using Microsoft.Extensions.DependencyInjection;
using App.CrossCutting;
using App.CrossCutting.IoC;
using App.Application.Services;
using App.Application.Interfaces;

namespace App.Broker
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var services = NativeInjectionBootStrapper.Configuration();
                var serviceProvider = services.BuildServiceProvider();
                var serviceData = new ServiceData(serviceProvider.GetService<IBrokerService>());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
