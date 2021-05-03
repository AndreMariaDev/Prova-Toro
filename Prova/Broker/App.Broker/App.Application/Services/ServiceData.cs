using App.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class ServiceData
    {
        private readonly IBrokerService _broker;

        public ServiceData(IBrokerService broker) 
        {
            _broker = broker;
            _broker.StartBroker();
        }
    }
}
