namespace MassTransit.Demo.Customers.Controllers
{
    using MassTransit.Demo.Customers.Commands;
    using MassTransit.Demo.Customers.Contracts.Events;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IBus _bus;

        public CustomersController(IBus bus)
        {
            this._bus = bus;
        }

        [HttpPost("register")]
        public async Task Register(CustomerRegistration.Command command, CancellationToken cancellationToken)
        {
            await this._bus.Publish<CustomerRegistrationEvent>(
                new
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow,
                    command.FirstName,
                    command.LastName,
                    command.Email,
                    command.Phone
                }, cancellationToken);
        }

        [HttpGet("activate/{id}/{activationKey}")]
        public async Task Activate([FromRoute] CustomerActvation.Command command, CancellationToken cancellationToken)
        {
            // check if activation key is valid, if so fire event.
            await this._bus.Publish<CustomerActvationEvent>(
                new
                {
                    command.Id,
                    command.ActivationKey
                }, cancellationToken);
        }
    }
}