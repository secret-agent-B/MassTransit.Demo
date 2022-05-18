namespace MassTransit.Demo.Communication
{
    internal class CustomEndpointNameFormatter : IEndpointNameFormatter
    {
        public string Separator => "_";

        public string Message<T>() where T : class
        {
            return $"MSG_{typeof(T).Name}";
        }

        public string SanitizeName(string name)
        {
            return name.ToLower();
        }

        public string TemporaryEndpoint(string tag)
        {
            return $"TEMP_EP_{tag.ToLower()}";
        }

        string IEndpointNameFormatter.CompensateActivity<T, TLog>()
        {
            return $"COMP_ACT_{typeof(T).Name}_{typeof(TLog).Name}";
        }

        string IEndpointNameFormatter.Consumer<T>()
        {
            return $"CONSUMER_{typeof(T).Name.Replace("Consumer", string.Empty)}";
        }

        string IEndpointNameFormatter.ExecuteActivity<T, TArguments>()
        {
            return $"ACTIVITY_{typeof(T).Name}_{typeof(TArguments).Name}";
        }

        string IEndpointNameFormatter.Saga<T>()
        {
            return $"SAGA_{typeof(T).Name}";
        }
    }
}