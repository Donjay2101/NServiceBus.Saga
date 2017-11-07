using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Saga.Messages;

namespace Saga.DispatchOrder
{
    public class DispatchOrderHandler:IHandleMessages<DispatchOrderCommand>
    {
        public async Task Handle(DispatchOrderCommand message, IMessageHandlerContext context)
        {
            Console.WriteLine("Order is dispatched.");
            ReplyOptions options = new ReplyOptions();            
            await context.Reply<IOrderDispatchedMessage>(e => { },options).ConfigureAwait(false);
            //await ReplyToOriginator(context, new OrderProcessedMessages()).ConfigureAwait(false);
            //MarkAsComplete();
        }
        
    }
}
