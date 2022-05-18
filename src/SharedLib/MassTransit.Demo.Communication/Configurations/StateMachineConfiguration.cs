using MassTransit.Demo.Communication.Exceptions;

namespace MassTransit.Demo.Communication.Configurations
{
    public class StateMachineConfiguration : IMessagingConfiguration
    {
        public string Connection { get; set; }

        public string DatabaseName { get; set; }

        public string Collection { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.Connection))
            {
                throw new MessagingConfigurationException($"Invalid {nameof(StateMachineConfiguration)} configuration. {nameof(this.Connection)} is empty.");
            }

            if (string.IsNullOrEmpty(this.DatabaseName))
            {
                throw new MessagingConfigurationException($"Invalid {nameof(StateMachineConfiguration)} configuration. {nameof(this.DatabaseName)} is empty.");
            }

            if (string.IsNullOrEmpty(this.Collection))
            {
                throw new MessagingConfigurationException($"Invalid {nameof(StateMachineConfiguration)} configuration. {nameof(this.Collection)} is empty.");
            }
        }
    }
}