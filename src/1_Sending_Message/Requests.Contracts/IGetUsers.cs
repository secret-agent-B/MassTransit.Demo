namespace Requests.Contracts
{
    public interface IGetUsers
    {
        int RoleId { get; }

        bool Enabled { get; }
    }
}
