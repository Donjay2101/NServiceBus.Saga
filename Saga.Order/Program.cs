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

            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();


            Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine("Service Started.");
            Console.Read();
        }
    }
}
