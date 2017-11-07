using System;
using System.Threading.Tasks;
using NServiceBus;
using Saga.Messages;

namespace Saga.Main
{
    public class OrderSaga : Saga<SagaData>,
        IAmStartedByMessages<PlaceOrderCommand>,
        IHandleMessages<OrderPlanned>,
        IHandleMessages<IOrderDispatchedMessage>,
        IHandleMessages<OrderProcessedMessage>
    {
        protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SagaData> mapper)
        {
            mapper.ConfigureMapping<PlaceOrderCommand>(msg => msg.OrderId)
                .ToSaga(s => s.OrderId);

            mapper.ConfigureMapping<OrderProcessedMessage>(message => message.OrderId)
                .ToSaga(sagaData => sagaData.OrderId);
        }

        public async Task Handle(PlaceOrderCommand message, IMessageHandlerContext context)
        {
            Console.WriteLine("Order has been placed......");

            Data.AddressFrom = message.AddressFrom;
            Data.AddressTo = message.AddressTo;
            Data.Price = message.Price;
            Data.Weight = message.Weight;

            var options = new SendOptions();
            options.SetDestination("Saga.Planning");

            await context.Send(new OrderPlanCommand { AddressTo = Data.AddressTo, OrderId = Data.OrderId }, options)
                .ConfigureAwait(false);
        }
        public async Task Handle(OrderPlanned message, IMessageHandlerContext context)
        {
            Console.WriteLine("Order has been planned......");

            var options = new SendOptions();
            options.SetDestination("Saga.Dispatcher");
            await context.Send(new DispatchOrderCommand
            {
                AddressTo = Data.AddressTo,
                Weight = Data.Weight
            }, options);
        }


        public async Task Handle(IOrderDispatchedMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine("Order has been dispatched......");
            await ReplyToOriginator(context, new OrderProcessedMessage { IsSuccess = true }).ConfigureAwait(false);
            MarkAsComplete();
        }

        public Task Handle(OrderProcessedMessage message, IMessageHandlerContext context)
        {
            Console.WriteLine("Order is Processed.");
            return Task.CompletedTask;
        }
    }
}
