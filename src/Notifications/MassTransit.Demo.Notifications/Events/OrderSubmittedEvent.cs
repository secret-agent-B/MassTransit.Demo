namespace MassTransit.Demo.Notifications.Contracts.Events
{
    public interface OrderSubmittedEvent
    {
        decimal TotalAmount { get; }

        Guid OrderId { get; }

        Guid CustomerId { get; }
    }
}