namespace MassTransit.Demo.Communication.Extensions
{
    using MassTransit.Demo.Communication.Configurations;
    using Microsoft.Extensions.Configuration;

    internal static class RabbitMQHelper
    {
        internal static void ConfigureRabbitMQ(this IBusRegistrationConfigurator serviceBusConfig, IConfigurationSection messagingConfigSection)
        {
            var config = messagingConfigSection.Get<RabbitMQConfiguration>();

            config.Validate();

            serviceBusConfig.UsingRabbitMq(
                 (ctx, cfg) =>
                 {
                     cfg.MessageTopology.SetEntityNameFormatter(new EntityNameFormatter());

                     cfg.Host(
                         new Uri(config.Host),
                         config.VirtualHost,
                         hostConfig =>
                         {
                             hostConfig.Username(config.Username);
                             hostConfig.Password(config.Password);
                             hostConfig.Heartbeat(config.Heartbeat);

                             // hostConfig.UseCluster(clusterConfig =>
                             // {
                             //     clusterConfig.Node("nodeX");
                             //     clusterConfig.Node("nodeY");
                             //     clusterConfig.Node("nodeZ");
                             // });
                         });

                     cfg.ConfigureEndpoints(ctx, new KebabCaseEndpointNameFormatter(false));
                 });
        }
    }
}