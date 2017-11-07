using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Saga.Messages
{
    public class OrderPlanCommand:IMessage
    {
         public Guid OrderId { get; set; }
        public string AddressTo { get; set; }
    }
}
