using MassTransit.Demo.Communication.Exceptions;

namespace MassTransit.Demo.Communication.Configurations
{
    public class MessagingTransportConfiguration : IMessagingConfiguration
    {
        public CommunicationTransport Transport { get; set; }

        public virtual void Validate()
        {
            if (this.Transport == CommunicationTransport.None)
            {
                throw new MessagingConfigurationException($"Invalid {nameof(MessagingTransportConfiguration)} configuration. {nameof(this.Transport)} is {CommunicationTransport.None}.");
            }
        }
    }
}