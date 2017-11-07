using System;
using System.Threading.Tasks;
using NServiceBus;
using Saga.Messages;

namespace Saga.OrderProcessed
{
    public class OrderProcessedEventHandler:IHandleMessages<OrderProcessedMessage>
    {
        public Task Handle(OrderProcessedMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine("Order is Processed.");
            return Task.CompletedTask;
        }
    }
}
