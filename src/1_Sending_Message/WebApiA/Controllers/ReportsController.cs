namespace WebApiA.Controllers
{
    using MassTransit;
    using MassTransit.Demo.Communication.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Requests.Contracts;
    using Requests.Contracts.DTOs;

    [ApiController]
    [Route("[controller]")]
    public class ReportsController : Controller
    {
        private IRequestClient<IGetUsers> getUsersClient;

        public ReportsController(IBus bus)
        {
            this.getUsersClient = bus.CreateRequestClient<IGetUsers>();
        }

        [HttpGet]
        public async Task<IActionResult> GetReports([FromQuery]int roleId, [FromQuery] bool enabled)
        {
            var busResponse = await this.getUsersClient.GetResponse<IListResponse<IUser>>(
                new
                {
                    RoleId = roleId,
                    Enabled = enabled
                });

            return Ok(busResponse.Message);
        }

        [HttpGet("error")]
        public async Task<IActionResult> GetReportsWithErrorHandling()
        {
            var (response, exception) = await this.getUsersClient.GetResponse<IListResponse<IUser>, IMessageConsumerException>(new
            {
                RoleId = 0, // Cause error
                Enabled = false
            });

            if (response.IsCompletedSuccessfully)
            {
                return Ok();
            }

            return BadRequest((await exception).Message);
        }
    }
}
