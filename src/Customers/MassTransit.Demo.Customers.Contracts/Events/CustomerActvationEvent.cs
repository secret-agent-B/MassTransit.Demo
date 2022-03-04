namespace MassTransit.Demo.Customers.Contracts.Events
{
    public interface CustomerActvationEvent
    {
        public Guid Id { get; set; }

        public string ActivationKey { get; set; }
    }
}