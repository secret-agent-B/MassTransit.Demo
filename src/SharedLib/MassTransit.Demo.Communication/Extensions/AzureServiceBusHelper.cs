namespace MassTransit.Demo.Communication.Extensions
{
    using MassTransit.Demo.Communication.Configurations;
    using Microsoft.Extensions.Configuration;

    internal static class AzureServiceBusHelper
    {
        internal static void ConfigureAzureServiceBus(this IBusRegistrationConfigurator serviceBusConfig, IConfigurationSection messagingConfigSection)
        {
            var config = messagingConfigSection.Get<ASBConfiguration>();

            config.Validate();

            serviceBusConfig.UsingAzureServiceBus(
                (ctx, cfg) =>
                {
                    cfg.Host(config.ConnectionString);
                    cfg.ConfigureEndpoints(ctx);
                });
        }
    }
}