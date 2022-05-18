using MassTransit.Demo.Communication.Exceptions;

namespace MassTransit.Demo.Communication.Configurations
{
    public class RabbitMQConfiguration : IMessagingConfiguration
    {
        public string Host { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public ushort Heartbeat { get; set; }

        public int Port { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(this.Host))
            {
                throw new MessagingConfigurationException($"Invalid {nameof(RabbitMQConfiguration)} configuration. {nameof(this.Host)} is empty.");
            }

            if (string.IsNullOrEmpty(this.Username))
            {
                throw new MessagingConfigurationException($"Invalid {nameof(RabbitMQConfiguration)} configuration. {nameof(this.Username)} is empty.");
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                throw new MessagingConfigurationException($"Invalid {nameof(RabbitMQConfiguration)} configuration. {nameof(this.Password)} is empty.");
            }

            const int minHeartbeat = 15;

            if (this.Heartbeat < minHeartbeat)
            {
                throw new MessagingConfigurationException($"Invalid {nameof(RabbitMQConfiguration)} configuration. {nameof(this.Heartbeat)} cannot be less than {minHeartbeat}.");
            }
        }
    }
}