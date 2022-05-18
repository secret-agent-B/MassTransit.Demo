namespace MassTransit.Demo.Notifications.Consumers
{
    using System.Threading.Tasks;
    using MassTransit.Demo.Orders.Contracts.Events;
    using Serilog;

    internal class OrderSubmittedConsumer
        : IConsumer<OrderSubmittedEvent>
    {
        private readonly ILogger _logger;

        public OrderSubmittedConsumer(ILogger logger)
        {
            this._logger = logger;
        }

        public Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            this._logger.Information("OrderSubmittedEvent {orderId} from customer {customerId} received", context.Message.OrderId, context.Message.CustomerId);
            return Task.CompletedTask;
        }
    }
}