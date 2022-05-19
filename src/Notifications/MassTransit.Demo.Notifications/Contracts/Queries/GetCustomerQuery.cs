namespace MassTransit.Demo.Notifications.Contracts.Queries
{
    using System;

    public interface GetCustomerQuery
    {
        Guid Id { get; }
    }
}