namespace MassTransit.Demo.Notifications.Consumers
{
    using System.Threading.Tasks;
    using MassTransit.Demo.Orders.Contracts.Events;
    using Serilog;

    internal class OrderSubmittedConsumer
        : IConsumer<OrderSubmittedEvent>
    {
        private readonly ILogger logger;

        public OrderSubmittedConsumer(ILogger logger)
        {
            this.logger = logger;
        }

        public Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            this.logger.Information("OrderSubmittedEvent {orderId} from customer {customerId} received", context.Message.OrderId, context.Message.CustomerId);

            return Task.CompletedTask;
        }
    }
}