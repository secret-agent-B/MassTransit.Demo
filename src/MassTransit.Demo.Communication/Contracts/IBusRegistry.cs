namespace MassTransit.Demo.Communication.Contracts
{
    using MassTransit.ExtensionsDependencyInjectionIntegration;

    /// <summary>
    /// Registers consumer within the project.
    /// </summary>
    public interface IBusRegistry
    {
        void RegisterConsumers(IServiceCollectionBusConfigurator configurator);
    }
}