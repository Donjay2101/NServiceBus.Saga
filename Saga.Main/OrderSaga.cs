using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NServiceBus;
using Saga.Messages;

namespace Saga.Main
{
    public class OrderSaga:Saga<SagaData>,
        IAmStartedByMessages<PlaceOrderCommand>,
        IHandleMessages<OrderPlanned>,
        IHandleMessages<IOrderDispatchedMessage>  ,
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
            Data.OrderId = message.OrderId;
            Data.AddressFrom = message.AddressFrom;
            Data.AddressTo = message.AddressTo;
            Data.Price = message.Price;
            Data.Weight = message.Weight;     
            SendOptions options = new SendOptions();
            options.SetDestination("Saga.Planning");
            await context.Send(new OrderPlanCommand() {AddressTo = Data.AddressTo, OrderId = Data.OrderId},options)
                .ConfigureAwait(false);
        }
        public async Task Handle(OrderPlanned message,IMessageHandlerContext context)
        {
            try
            {
                SendOptions options = new SendOptions();
                options.SetDestination("Saga.Dispatcher");
                await context.Send(new DispatchOrderCommand
                {
                    AddressTo = Data.AddressTo,
                    Weight = Data.Weight
                }, options);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }


        public async Task Handle(IOrderDispatchedMessage message, IMessageHandlerContext context)
        {
            try
            {
                await Task.Run(() =>
                {
                    Console.WriteLine("Order has been dispathced......");
                     ReplyToOriginator(context, new OrderProcessedMessage() { IsSuccess = true }).ConfigureAwait(false);
                    MarkAsComplete();
                });
               
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        public Task Handle(OrderProcessedMessage message, IMessageHandlerContext context)
        {
            try
            {
                 Task.Run(() =>
                {
                    Console.WriteLine("Order is Processed.");
                });
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


    }
}
