namespace MassTransit.Demo.Communication.Contracts
{
    /// <summary>
    /// Registers consumer within the project.
    /// </summary>
    public interface IBusRegistry
    {
        void RegisterConsumers(IBusRegistrationConfigurator configurator);
    }
}