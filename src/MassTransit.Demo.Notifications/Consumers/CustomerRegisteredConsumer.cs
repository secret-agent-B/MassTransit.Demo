namespace MassTransit.Demo.Notifications.Consumers
{
    using MassTransit.Demo.Customers.Contracts.Events;
    using Serilog;
    using System.Threading.Tasks;

    internal class CustomerRegisteredConsumer
        : IConsumer<CustomerRegisteredEvent>
    {
        private readonly ILogger _logger;

        public CustomerRegisteredConsumer(ILogger logger)
        {
            this._logger = logger;
        }

        public Task Consume(ConsumeContext<CustomerRegisteredEvent> context)
        {
            this._logger.Information($"{typeof(CustomerRegisteredConsumer).Name} consumed: Sending activation key to user {context.Message.Id}");
            return Task.CompletedTask;
        }
    }
}