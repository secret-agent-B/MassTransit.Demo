namespace MassTransit.Demo.Notifications.Consumers
{
    using System.Threading.Tasks;
    using MassTransit.Demo.Notifications.Contracts.DTOs;
    using MassTransit.Demo.Notifications.Contracts.Events;
    using MassTransit.Demo.Notifications.Contracts.Queries;
    using Serilog;

    public class OrderSubmittedConsumer
        : IConsumer<OrderSubmittedEvent>
    {
        private readonly ILogger _logger;
        private readonly IRequestClient<GetCustomerQuery> _getCustomerClient;

        public OrderSubmittedConsumer(ILogger logger, IRequestClient<GetCustomerQuery> getCustomerClient)
        {
            this._logger = logger;
            this._getCustomerClient = getCustomerClient;
        }

        public async Task Consume(ConsumeContext<OrderSubmittedEvent> context)
        {
            var getCustomerResponse = await this._getCustomerClient.GetResponse<Customer>(new
            {
                Id = context.Message.CustomerId
            });

            this._logger.Information(
                "OrderSubmittedEvent consumed: Order {orderId} from customer ({customerName}) {customerId} for {totalAmount} was received,",
                context.Message.OrderId,
                getCustomerResponse.Message.FirstName,
                context.Message.TotalAmount,
                context.Message.CustomerId);
        }
    }
}