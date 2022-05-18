namespace MassTransit.Demo.Orders.Contracts.Events
{
    public interface OrderSubmittedEvent
    {
        decimal TotalPrice { get; }

        Guid OrderId { get; }

        Guid CustomerId { get; }
    }
}