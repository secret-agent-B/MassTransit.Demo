namespace WebApiA.Controllers
{
    using MassTransit;
    using MassTransit.Demo.Communication.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using Requests.Contracts;
    using Requests.Contracts.DTOs;
    using System.Text;

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
            var busResponse = await this.getUsersClient.GetResponse<IList<IUser>>(new
            {
                RoleId = roleId,
                Enabled = enabled
            });

            var reportBuilder = new StringBuilder();
            reportBuilder.AppendLine("User Report");

            foreach (var user in busResponse.Message)
            {
                reportBuilder.AppendLine($"------------------------");
                reportBuilder.AppendLine($"Id: {user.Id}");
                reportBuilder.AppendLine($"Name: {user.Name}");
                reportBuilder.AppendLine($"Role: {user.Role}");

                var isEnabled = user.IsEnabled ? "Yes" : "No";
                reportBuilder.AppendLine($"IsEnabled: {isEnabled}");
            }

            reportBuilder.AppendLine($"------------------------");
            return Ok(reportBuilder.ToString());
        }

        [HttpGet("error")]
        public async Task<IActionResult> GetReportsWithErrorHandling([FromQuery] bool enabled)
        {
            var (response, exception) = await this.getUsersClient.GetResponse<IList<IUser>, IMessageConsumerException>(new
            {
                RoleId = 0, // Cause error
                Enabled = enabled
            });

            if (response.IsCompletedSuccessfully)
            {
                return Ok();
            }

            return BadRequest((await exception).Message);
        }
    }
}
