namespace MassTransit.Demo.Communication
{
    public class EntityNameFormatter : IEntityNameFormatter
    {
        public string FormatEntityName<T>()
        {
            return typeof(T).Name;
        }
    }
}