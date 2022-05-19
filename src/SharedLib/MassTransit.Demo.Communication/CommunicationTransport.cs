namespace MassTransit.Demo.Communication
{
    public enum CommunicationTransport
    {
        None,
        InMemory,
        AzureServiceBus,
        RabbitMQ,
        AmazonSQS,
    }
}