namespace WebApiB.Consumers
{
    using Bogus;
    using MassTransit;
    using MassTransit.Demo.Communication.Contracts;
    using Requests.Contracts;
    using Requests.Contracts.DTOs;
    using System.Threading.Tasks;
    using WebApiB.Domain;

    public class GetUsersConsumer : IConsumer<IGetUsers>
    {
        private static readonly IList<Role> roles = new List<Role>
            {
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                },
                new Role
                {
                    Id = 2,
                    Name = "User",
                }
            };

        public async Task Consume(ConsumeContext<IGetUsers> context)
        {
            // role id is invalid, return an error message.
            if (!roles.Any(x => x.Id == context.Message.RoleId))
            {
                await context.RespondAsync<IMessageConsumerException>(new
                {
                    ErrorMessage = "Role not found"
                });
            }

            // build the response with fake values.
            var faker = new Faker<User>();

            faker
                .RuleFor(x=>x.Id, f=> f.Random.Int())
                .RuleFor(x=>x.Name, f=> f.Person.FirstName)
                .RuleFor(x=>x.Email, f=>f.Person.Email)
                .RuleFor(x=>x.IsEnabled, context.Message.Enabled)
                .RuleFor(x=>x.Role, roles.Single(x => x.Id == context.Message.RoleId).Name);

            // send the response.
            await context.RespondAsync<IListResponse<IUser>>(new
            {
                Items = faker.Generate(10)
            });
        }
    }
}
