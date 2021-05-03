using App.Application.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class ProcessMessage
    {
        public static void ConsumerReceived(
                object sender,
                BasicDeliverEventArgs args,
                IWorkflowService workflowService,
                IModel channel
        )
        {
            try
            {
                var message = Encoding.UTF8.GetString(args.Body.ToArray());
                workflowService.Process(message);
            }
            catch (Exception ex)
            {
                channel.BasicNack(args.DeliveryTag, false,false);
            }

        }
    }
}
