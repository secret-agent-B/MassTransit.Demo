namespace MassTransit.Demo.Orders.Contracts.DTOs
{
    public interface Order
    {
        public Guid Id { get; }

        public Guid CustomerId { get; }

        public DateTime CreatedOn { get; }

        public decimal Price { get; }

        public List<string> ProductIds { get; set; }
    }
}