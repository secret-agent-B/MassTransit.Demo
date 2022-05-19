namespace MassTransit.Demo.Orders.Contracts.DTOs
{
    public interface Order
    {
        Guid Id { get; }

        Guid CustomerId { get; }

        DateTime CreatedOn { get; }

        decimal TotalAmount { get; }

        List<string> ProductIds { get; }
    }
}