namespace MassTransit.Demo.Communication.Contracts
{
    public interface MessagingConsumerException
    {
        List<string> Errors { get; }
    }
}