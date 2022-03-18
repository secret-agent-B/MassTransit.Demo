namespace MassTransit.Demo.Communication.Contracts
{
    using System.Collections.Generic;

    public interface IListResponse<T>
    {
        IList<T> Items { get; }
    }
}
