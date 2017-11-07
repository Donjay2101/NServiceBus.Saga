using System;
using System.Threading.Tasks;
using NServiceBus;
using Saga.Messages;

namespace Saga.planning
{
    class PlanOrderHandler : IHandleMessages<OrderPlanCommand>
    {
        public async Task Handle(OrderPlanCommand message, IMessageHandlerContext context)
        {
            Console.WriteLine($"OrderId {message.OrderId} planned");
            //Do planning
            await context.Reply<OrderPlanned>(msg => { }).ConfigureAwait(false);
        }
    }
}
