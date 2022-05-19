namespace MassTransit.Demo.Customers.Contracts.Events
{
    public interface CustomerActvationEvent
    {
        Guid Id { get; set; }

        string ActivationKey { get; set; }
    }
}