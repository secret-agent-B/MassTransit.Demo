namespace MassTransit.Demo.Communication
{
    public class ServiceBusConfiguration
    {
        internal const string Section = "ServiceBusConfiguration";

        public ushort Heartbeat { get; set; }

        public string Host { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }
    }
}