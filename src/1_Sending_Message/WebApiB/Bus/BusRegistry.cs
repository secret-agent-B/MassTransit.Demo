namespace WebApiB.Bus
{
    using MassTransit.Demo.Communication.Contracts;
    using MassTransit.ExtensionsDependencyInjectionIntegration;
    using WebApiB.Consumers;

    public class BusRegistry : IBusRegistry
    {
        public void RegisterConsumers(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddConsumer<GetUsersConsumer>();
        }
    }
}
