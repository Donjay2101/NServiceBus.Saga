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
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();

            Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();

            Console.WriteLine("Service Started.");

            Console.Read();
        }
    }
}
