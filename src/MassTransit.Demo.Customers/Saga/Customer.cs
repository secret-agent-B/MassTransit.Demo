namespace MassTransit.Demo.Customers.Saga
{
    using System;
    using Automatonymous;
    using MassTransit.Saga;

    public class Customer :
        SagaStateMachineInstance,
        ISagaVersion
    {
        public Guid CorrelationId { get; set; }

        public int CurrentState { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ActivatedOn { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public int Version { get; set; }
    }
}