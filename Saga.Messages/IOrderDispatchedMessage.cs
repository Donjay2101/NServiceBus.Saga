using NServiceBus;

namespace Saga.Messages
{
    public interface IOrderDispatchedMessage:IMessage
    {
        bool Result { get; set; }
    }
}
