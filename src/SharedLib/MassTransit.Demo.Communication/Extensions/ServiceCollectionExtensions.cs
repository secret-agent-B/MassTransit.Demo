namespace MassTransit.Demo.Communication.Extensions
{
    using MassTransit.Demo.Communication.Configurations;
    using MassTransit.Demo.Communication.Contracts;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization;
    using MongoDB.Bson.Serialization.Serializers;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMassTransitMiddleware(
            this IServiceCollection services,
            Action<IBusRegistrationConfigurator, IConfiguration> busRegConfig)
        {
            var config = services.BuildServiceProvider().GetRequiredService<IConfiguration>();

            services.AddMassTransit(serviceCollectionBusConfig => busRegConfig(serviceCollectionBusConfig, config));

            return services;
        }

        public static IBusRegistrationConfigurator ConfigureBus<TConsumerAssembly>(
            this IBusRegistrationConfigurator serviceCollectionBusConfig,
            IConfiguration config)
        {
            var messagingConfigSection = config.GetSection("MessagingConfiguration");
            var messagingTransportConfiguration = messagingConfigSection.Get<MessagingTransportConfiguration>();

            messagingTransportConfiguration.Validate();

            switch (messagingTransportConfiguration.Transport)
            {
                case CommunicationTransport.RabbitMQ:
                    RabbitMQHelper.ConfigureRabbitMQ(serviceCollectionBusConfig, messagingConfigSection);
                    break;

                case CommunicationTransport.AzureServiceBus:
                    AzureServiceBusHelper.ConfigureAzureServiceBus(serviceCollectionBusConfig, messagingConfigSection);
                    break;

                case CommunicationTransport.InMemory:
                    InMemoryBusHelper.ConfigureInMemory(serviceCollectionBusConfig);
                    break;
            }

            serviceCollectionBusConfig.AddConsumers(typeof(TConsumerAssembly).Assembly);

            return serviceCollectionBusConfig;
        }

        public static IBusRegistrationConfigurator ConfigureSaga<TStateMachine, TState>(
            this IBusRegistrationConfigurator serviceCollectionBusConfig,
            IConfiguration config)
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
    }
}