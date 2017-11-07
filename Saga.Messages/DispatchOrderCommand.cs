using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using NServiceBus;

namespace Saga.Messages
{
    public class DispatchOrderCommand:ICommand
    {
        public string AddressTo { get; set; }
        public int Weight { get; set; }
    }
}
