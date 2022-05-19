namespace MassTransit.Demo.Customers.Contracts.DTOs
{
    using System;

    public interface Customer
    {
        Guid Id { get; }

        string FirstName { get; }

        string LastName { get; }

        Address Address { get; }

        string PhoneNumber { get; }

        string Email { get; }
    }
}