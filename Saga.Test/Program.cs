using System;
using NServiceBus;
using NServiceBus.Routing;
using Saga.Messages;

namespace Saga.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Test";
                var endpointConfiguration = new EndpointConfiguration("Saga.Test");
                endpointConfiguration.UsePersistence<InMemoryPersistence>();
                endpointConfiguration.UseTransport<RabbitMQTransport>().UseConventionalRoutingTopology().ConnectionString("host=localhost;username=guest;password=guest");
                endpointConfiguration.PurgeOnStartup(true);
                endpointConfiguration.EnableInstallers();
                endpointConfiguration.MakeInstanceUniquelyAddressable("uniqueId");
                var instance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();

                SendOptions options = new SendOptions();
                options.SetDestination("Saga.Main");
                instance.Send(new PlaceOrderCommand
                {
                    OrderId = Guid.NewGuid(),
                    AddressFrom = "Malaysia",
                    AddressTo = "India",
                    Price = 1000,
                    Weight = 2
                },options).ConfigureAwait(false);

                Console.WriteLine("Started.");
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
