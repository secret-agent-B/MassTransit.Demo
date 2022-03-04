namespace MassTransit.Demo.Communication.Extensions
{
    using MassTransit.ExtensionsDependencyInjectionIntegration;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransitMiddleware(
            this IServiceCollection services,
            Action<IServiceCollectionBusConfigurator, IConfiguration> busConfig)
        {
            services
                .AddMassTransit(serviceCollectionBusConfig =>
                {
                    serviceCollectionBusConfig.SetKebabCaseEndpointNameFormatter();

                    serviceCollectionBusConfig.AddBus(ctx => Bus.Factory.CreateUsingRabbitMq(rmqCfg =>
                    {
                        var serviceBusConfiguration = services
                            .BuildServiceProvider()
                            .GetRequiredService<IConfiguration>()
                            .GetSection(ServiceBusConfiguration.Section)
                            .Get<ServiceBusConfiguration>();

                        rmqCfg.Host(
                            new Uri($"amqp://{serviceBusConfiguration.Host}"),
                            hostConfig =>
                            {
                                hostConfig.Username(serviceBusConfiguration.Username);
                                hostConfig.Password(serviceBusConfiguration.Password);
                                hostConfig.Heartbeat(serviceBusConfiguration.Heartbeat);
                            });

                        rmqCfg.ConfigureEndpoints(ctx);
                    }));

                    busConfig(serviceCollectionBusConfig, services.BuildServiceProvider().GetRequiredService<IConfiguration>());

                    serviceCollectionBusConfig.AddPublishMessageScheduler();
                })
                .AddMassTransitHostedService();

            return services;
        }
    }
}