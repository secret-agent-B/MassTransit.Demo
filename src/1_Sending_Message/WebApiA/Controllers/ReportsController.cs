namespace WebApiA.Controllers
{
    using MassTransit;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ReportsController : Controller
    {
        public ReportsController(IBus bus)
        {
            Bus = bus;
        }

        public IBus Bus { get; }

        [HttpGet]
        public IActionResult GetReports()
        {
            return Ok();
        }
    }
}
