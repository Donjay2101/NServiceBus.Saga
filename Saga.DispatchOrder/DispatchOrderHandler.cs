using System;
using System.Threading.Tasks;
using NServiceBus;
using Saga.Messages;

namespace Saga.DispatchOrder
{
    public class DispatchOrderHandler : IHandleMessages<DispatchOrderCommand>
    {
        public Task Handle(DispatchOrderCommand message, IMessageHandlerContext context)
        {
            Console.WriteLine("Order is dispatched.");

            return context.Reply<IOrderDispatchedMessage>(e => { });
        }

    }
}
