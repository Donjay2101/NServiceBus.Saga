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
            endpointConfiguration.UsePersistence<LearningPersistence>();
            endpointConfiguration.UseTransport<LearningTransport>();

            Endpoint.Start(endpointConfiguration).ConfigureAwait(false).GetAwaiter().GetResult();
            Console.Read();
        }
    }
}
