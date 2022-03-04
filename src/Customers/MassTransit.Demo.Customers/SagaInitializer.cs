namespace MassTransit.Demo.Customers
{
    using MassTransit.Demo.Customers.Contracts.Events;
    using MassTransit.Topology.Topologies;

    internal static class SagaInitializer
    {
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            GlobalTopology.Send.UseCorrelationId<CustomerRegistrationEvent>(x => x.Id);
            GlobalTopology.Send.UseCorrelationId<CustomerActvationEvent>(x => x.Id);
            GlobalTopology.Send.UseCorrelationId<CustomerRegisteredEvent>(x => x.Id);

            _initialized = true;
        }
    }
}