namespace MassTransit.Demo.Communication.Exceptions
{
    public class MessagingConfigurationException : Exception
    {
        public MessagingConfigurationException(string message)
            : base(message)
        {
        }
    }
}