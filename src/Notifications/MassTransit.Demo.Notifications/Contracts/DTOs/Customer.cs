namespace MassTransit.Demo.Notifications.Contracts.DTOs
{
    using System;

    public interface Customer
    {
        Guid Id { get; }

        string FirstName { get; }

        string PhoneNumber { get; }

        string Email { get; }
    }
}