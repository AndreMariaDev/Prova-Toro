using App.Shared.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broker
{
    public interface IBrokerService
    {
        void SendQueue<T>(T entity, string queueName, RabbitMQConfigurations rabbitMQConfigurations);
    }
}
