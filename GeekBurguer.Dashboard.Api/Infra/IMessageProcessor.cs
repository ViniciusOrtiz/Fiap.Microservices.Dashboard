using Azure.Messaging.ServiceBus;

namespace GeekBurguer.Dashboard.Api.Infra
{
    public interface IMessageProcessor
    {
        Task ProcessMessage(ServiceBusReceivedMessage message);
    }
}