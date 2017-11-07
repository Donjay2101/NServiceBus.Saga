using System;
using NServiceBus;

namespace Saga.Order
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Palnning";
            var endpointConfiguration = new EndpointConfiguration("Saga.Planning");
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseTransport<RabbitMQTransport>().UseConventionalRoutingTopology().ConnectionString("host=localhost;username=guest;password=guest");
            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.MakeInstanceUniquelyAddressable("uniqueId");
            var instance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.WriteLine("Service Started.");
            Console.Read();
        }
    }
}
