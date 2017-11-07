using System;
using NServiceBus;

namespace Saga.Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Main";
            var endpointConfiguration = new EndpointConfiguration("Saga.Main");
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
