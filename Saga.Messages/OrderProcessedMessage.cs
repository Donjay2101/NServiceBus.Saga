using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Saga.Messages
{
    public class OrderProcessedMessage:IMessage
    {
        public Guid OrderId { get; set; }
        public string AddressFrom { get; set; }
        public string AddressTo { get; set; }
        public int Weight { get; set; }
        public int Price { get; set; }

        public bool IsSuccess { get; set; }
    }
}
