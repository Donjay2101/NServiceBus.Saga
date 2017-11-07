using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Saga.Main
{
    public class SagaData:ContainSagaData
    {
        public Guid OrderId { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }
    }
}
