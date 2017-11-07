using System;
using NServiceBus;

namespace Saga.OrderProcessed
{
    using System.Threading.Tasks;
    using Messages;

    class Program
    {
        static void Main(string[] args)
        {
            AsyncMain().GetAwaiter().GetResult();
        }

        static async Task AsyncMain()
        {
            try
            {
                Console.Title = "Processed";
                var endpointConfiguration = new EndpointConfiguration("Saga.Processed");
                endpointConfiguration.UsePersistence<LearningPersistence>();
                endpointConfiguration.UseTransport<LearningTransport>();

                var instance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

                Console.WriteLine("Started.");

                var options = new SendOptions();
                options.SetDestination("Saga.Main");

                await instance.Send(new PlaceOrderCommand
                {
                    OrderId = Guid.NewGuid(),
                    AddressFrom = "Malaysia",
                    AddressTo = "India",
                    Price = 1000,
                    Weight = 2
                }, options).ConfigureAwait(false);

                Console.WriteLine("Place order command sent.");
                Console.Read();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
