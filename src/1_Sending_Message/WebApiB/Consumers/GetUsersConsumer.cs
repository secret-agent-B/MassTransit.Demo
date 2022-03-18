namespace WebApiB.Consumers
{
    using Bogus;
    using MassTransit;
    using MassTransit.Demo.Communication.Contracts;
    using Requests.Contracts;
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
            if (!roles.Any(x=>x.Id == context.Message.RoleId))
            {
                await context.RespondAsync<IMessageConsumerException>(new
                {
                    ErrorMessage = "Role not found"
                });
            }

            // build the response with fake values.
            var faker = new Faker();
            var response = new List<User>();

            foreach (var id in Enumerable.Range(1, 10))
            {
                response.Add(new User
                {
                    Id = faker.Random.Int(),
                    Name = faker.Person.FullName,
                    Email = faker.Person.Email,
                    IsEnabled = faker.PickRandom<bool>(),
                    Role = roles.Single(x => x.Id == context.Message.RoleId).Name,
                });
            }

            // send the response.
            await context.RespondAsync<IList<User>>(response);
        }
    }
}
