namespace MassTransit.Demo.Orders.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IBus bus;

        public OrdersController(IBus bus)
        {
            this.bus = bus;
        }
    }
}