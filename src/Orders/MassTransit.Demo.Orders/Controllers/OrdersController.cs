namespace MassTransit.Demo.Orders.Controllers
{
    using MassTransit.Demo.Orders.Commands;
    using MassTransit.Demo.Orders.Domains.Orders;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<Order> SubmitOrder(CreateOrder.Command cmd, CancellationToken cancellationToken)
            => await this._mediator.Send(cmd, cancellationToken);
    }
}