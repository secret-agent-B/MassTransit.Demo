namespace MassTransit.Demo.Customers.Consumers
{
    using System.Threading.Tasks;
    using Bogus;
    using MassTransit.Demo.Communication.Contracts;
    using MassTransit.Demo.Customers.Contracts.DTOs;
    using MassTransit.Demo.Customers.Contracts.Queries;

    public class GetCustomerConsumer
        : IConsumer<GetCustomerQuery>
    {
        public async Task Consume(ConsumeContext<GetCustomerQuery> context)
        {
            if (context.Message.Id == Guid.Empty)
            {
                await context.RespondAsync<MessagingConsumerException>(new
                {
                    Errors = new[] { $"Customer Id is invalid {context.Message.Id}." }
                });

                return;
            }

            var faker = new Faker();

            await context.RespondAsync<Customer>(new
            {
                Id = context.Message.Id,
                FirstName = faker.Person.FirstName,
                LastName = faker.Person.LastName,
                Address = new
                {
                    Street1 = faker.Address.StreetAddress(),
                    Street2 = string.Empty,
                    City = faker.Address.City(),
                    State = faker.Address.StateAbbr(),
                    PostalCode = faker.Address.ZipCode()
                },
                PhoneNumber = faker.Phone.PhoneNumber(),
                Email = faker.Person.Email
            });
        }
    }
}