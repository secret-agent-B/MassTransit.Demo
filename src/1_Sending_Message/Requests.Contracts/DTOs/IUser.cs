namespace Requests.Contracts.DTOs
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        string Email { get; }

        string Role { get; }

        bool IsEnabled { get; }
    }
}
