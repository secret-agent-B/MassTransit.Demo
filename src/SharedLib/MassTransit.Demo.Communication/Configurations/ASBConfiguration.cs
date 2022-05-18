using MassTransit.Demo.Communication.Exceptions;

namespace MassTransit.Demo.Communication.Configurations
{
    public class ASBConfiguration : IMessagingConfiguration
    {
        public string ConnectionString { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.ConnectionString))
            {
                throw new MessagingConfigurationException($"Invalid {nameof(ASBConfiguration)} configuration. {nameof(this.ConnectionString)} is empty.");
            }
        }
    }
}