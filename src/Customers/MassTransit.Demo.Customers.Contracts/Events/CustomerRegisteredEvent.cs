namespace MassTransit.Demo.Customers.Contracts.Events
{
    public interface CustomerRegisteredEvent
    {
        Guid Id { get; }

        string FirstName { get; }

        string LastName { get; }

        string Email { get; }

        string Phone { get; }

        DateTime CreatedOn { get; }
    }
}