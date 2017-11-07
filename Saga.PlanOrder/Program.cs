using System;
using NServiceBus;

namespace Saga.OrderProcessed
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Processed";
                var endpointConfiguration = new NServiceBus.EndpointConfiguration("Saga.Processed");
                endpointConfiguration.UsePersistence<InMemoryPersistence>();
                endpointConfiguration.UseTransport<RabbitMQTransport>().UseConventionalRoutingTopology().ConnectionString("host=localhost;username=guest;password=guest");
                //endpointConfiguration.PurgeOnStartup(true);
                endpointConfiguration.EnableInstallers();
                endpointConfiguration.MakeInstanceUniquelyAddressable("uniqueId");
                var instance = Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();                
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
