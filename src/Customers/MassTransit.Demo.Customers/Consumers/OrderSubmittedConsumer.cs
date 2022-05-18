namespace MassTransit.Demo.Customers.Consumers
{
    using MassTransit.Demo.Orders.Contracts.Events;
    using Serilog;

    public class OrderSubmittedConsumer
        : IConsumer<OrderSubmittedEvent>
    {
        private readonly ILogger _logger;

        public OrderSubmittedConsumer(ILogger logger)
        {
            this._logger = logger;
        }

        public  Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            this._logger.Information(
                "OrderSubmittedEvent consumed: Let's add the order {orderId} to customer {customerId}",
                context.Message.OrderId,
                context.Message.CustomerId);

            return Task.CompletedTask;
        }
    }
}
