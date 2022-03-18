namespace MassTransit.Demo.Communication.Contracts
{
    public interface IMessageConsumerException
    {
        string ErrorMessage { get; }
    }
}
