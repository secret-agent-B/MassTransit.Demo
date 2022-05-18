namespace MassTransit.Demo.Communication.Extensions
{
    using Automatonymous;
    using MassTransit.Demo.Communication.Configurations;
    using MassTransit.ExtensionsDependencyInjectionIntegration;
    using MassTransit.Saga;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransitMiddleware(
            this IServiceCollection services,
            Action<IServiceCollectionBusConfigurator, IConfiguration> busConfig)
        {
            var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.AddMassTransit(serviceCollectionBusConfig => serviceCollectionBusConfig.ConfigureBus(config));

            return services;
        }

        public static IServiceCollectionBusConfigurator ConfigureBus(this IServiceCollectionBusConfigurator serviceCollectionBusConfig, IConfiguration config)
        {
            var messagingConfigSection = config.GetSection("MessagingConfiguration");
            var messagingTransportConfiguration = messagingConfigSection.Get<MessagingTransportConfiguration>();

            messagingTransportConfiguration.Validate();

            switch (messagingTransportConfiguration.Transport)
            {
                case CommunicationTransport.RabbitMQ:
                    serviceCollectionBusConfig.ConfigureRabbitMQ(messagingConfigSection);
                    break;

                case CommunicationTransport.AzureServiceBus:
                    serviceCollectionBusConfig.ConfigureAzureServiceBus(messagingConfigSection);
                    break;

                case CommunicationTransport.InMemory:
                    serviceCollectionBusConfig.ConfigureInMemory();
                    break;
            }

            return serviceCollectionBusConfig;
        }

        public static IServiceCollectionBusConfigurator ConfigureSaga<TStateMachine, TState>(this IServiceCollectionBusConfigurator serviceCollectionBusConfig, IConfiguration config)
            where TStateMachine : class, SagaStateMachine<TState>
            where TState : class, SagaStateMachineInstance, ISagaVersion
        {
            var stateMachineConfig = config
                    .GetSection(nameof(StateMachineConfiguration))
                    .Get<StateMachineConfiguration>();

            stateMachineConfig.Validate();

            serviceCollectionBusConfig.AddSagaStateMachine<TStateMachine, TState>()
                .MongoDbRepository(
                    cfg =>
                    {
                        cfg.Connection = stateMachineConfig.Connection;
                        cfg.DatabaseName = stateMachineConfig.DatabaseName;
                        cfg.CollectionName = stateMachineConfig.Collection;

                        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
                    });

            return serviceCollectionBusConfig;
        }

        private static void ConfigureRabbitMQ(this IServiceCollectionBusConfigurator serviceBusConfig, IConfigurationSection messagingConfigSection)
        {
            var config = messagingConfigSection.Get<RabbitMQConfiguration>();

            config.Validate();

            serviceBusConfig.UsingRabbitMq(
                 (ctx, rmqCfg) =>
                 {
                     rmqCfg.Host(
                         new Uri($"amqp://{config.Host}"),
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
                 });
        }

        private static void ConfigureAzureServiceBus(this IServiceCollectionBusConfigurator serviceBusConfig, IConfigurationSection messagingConfigSection)
        {
            var config = messagingConfigSection.Get<ASBConfiguration>();

            config.Validate();

            serviceBusConfig.UsingAzureServiceBus(
                (ctx, asbConfig) =>
                {
                    asbConfig.Host(config.ConnectionString);
                });
        }

        private static void ConfigureInMemory(this IServiceCollectionBusConfigurator serviceBusConfig)
        {
            serviceBusConfig.UsingInMemory(
                (ctx, inMemConfig) =>
                {
                    inMemConfig.ConfigureEndpoints(ctx);
                });
        }
    }
}