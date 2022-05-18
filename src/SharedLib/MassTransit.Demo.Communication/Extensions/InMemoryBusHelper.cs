namespace MassTransit.Demo.Communication.Extensions
{
    internal static class InMemoryBusHelper
    {
        internal static void ConfigureInMemory(this IBusRegistrationConfigurator serviceBusConfig)
        {
            serviceBusConfig.UsingInMemory(
                (ctx, inMemConfig) =>
                {
                    inMemConfig.ConfigureEndpoints(ctx);
                });
        }
    }
}