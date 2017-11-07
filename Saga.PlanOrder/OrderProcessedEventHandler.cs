using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Saga.Messages;

namespace Saga.OrderProcessed
{
    public class OrderProcessedEventHandler:IHandleMessages<OrderProcessedMessage>
    {
        public async Task Handle(OrderProcessedMessage message, IMessageHandlerContext context)
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Order is Processed.");
            });                     
        }
    }
}
