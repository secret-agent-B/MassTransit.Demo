namespace MassTransit.Demo.Customers.Contracts.DTOs
{
    public interface Address
    {
        string Street1 { get; }

        string Street2 { get; }

        string City { get; }

        string State { get; }

        string PostalCode { get; }
    }
}