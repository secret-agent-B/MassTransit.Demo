namespace MassTransit.Demo.Orders.Domains.Orders
{
    public class Order
    {
        public Order()
        {
            this.ProductIds = new List<string>();
        }

        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public DateTime CreatedOn { get; set; }

        public decimal TotalAmount { get; set; }

        public List<string> ProductIds { get; set; }
    }
}