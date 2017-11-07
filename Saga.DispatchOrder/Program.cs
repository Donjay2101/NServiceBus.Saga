using System;
using NServiceBus;

namespace Saga.DispatchOrder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Dispatcher";
            var endpointConfiguration = new EndpointConfiguration("Saga.Dispatcher");
            endpointConfiguration.UsePersistence<InMemoryPersistence>();
            endpointConfiguration.UseTransport<RabbitMQTransport>().UseConventionalRoutingTopology().ConnectionString("host=localhost;username=guest;password=guest");
            endpointConfiguration.PurgeOnStartup(true);
            endpointConfiguration.EnableInstallers();
            endpointConfiguration.MakeInstanceUniquelyAddressable("uniqueId");
            var instance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.Read();
        }
    }
}
