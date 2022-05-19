namespace MassTransit.Demo.Orders.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using MassTransit.Demo.Orders.Contracts.Events;
    using MassTransit.Demo.Orders.Domains.Orders;
    using MediatR;

    public class CreateOrder
    {
        public class Command : IRequest<Order>
        {
            public Command()
            {
                this.ProductIds = new List<string>();
            }

            public Guid CustomerId { get; set; }

            public decimal TotalAmount { get; set; }

            public List<string> ProductIds { get; set; }
        }

        public class Handler : IRequestHandler<Command, Order>
        {
            private readonly IPublishEndpoint _publishEndpoint;
            private readonly IBus _bus;

            public Handler(IPublishEndpoint publishEndpoint, IBus bus)
            {
                this._publishEndpoint = publishEndpoint;
                this._bus = bus;
            }

            public async Task<Order> Handle(Command request, CancellationToken cancellationToken)
            {
                // Create a dummy order
                var order = new Order
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow,
                    CustomerId = request.CustomerId,
                    ProductIds = request.ProductIds,
                    TotalAmount = request.TotalAmount
                };

                // Publish an event to the bus with the new order info
                await this._publishEndpoint.Publish<OrderSubmittedEvent>(new
                {
                    TotalAmount = order.TotalAmount,
                    OrderId = order.Id,
                    CustomerId = order.CustomerId
                }, cancellationToken);

                await this._bus.Publish<OrderSubmittedEvent>(new
                {
                    TotalAmount = order.TotalAmount + 10m,
                    OrderId = order.Id,
                    CustomerId = order.CustomerId
                }, cancellationToken);

                // Return the order back
                return order;
            }
        }
    }
}