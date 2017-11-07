using System;
using System.Collections.Generic;
using System.Text;
using NServiceBus;

namespace Saga.Messages
{
    public interface IOrderDispatchedMessage:IMessage
    {
        bool Result { get; set; }
    }
}
