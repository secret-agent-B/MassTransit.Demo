namespace MassTransit.Demo.Customers.Saga
{
    using MassTransit.Demo.Customers.Contracts.Events;

    public class CustomerStateMachine
        : MassTransitStateMachine<Customer>
    {
        static CustomerStateMachine()
        {
            SagaInitializer.Initialize();
        }

        public CustomerStateMachine()
        {
            this.InstanceState(x => x.CurrentState, this.PendingActivation, this.Active, this.Inactive);

            this.Event(() => this.CustomerRegistrationEvent, c => c.CorrelateById(x => x.Message.Id));
            this.Event(() => this.CustomerActivatedEvent, c => c.CorrelateBy((Saga, context) => Saga.CorrelationId == context.Message.Id));

            this.Initially(
                this.When(this.CustomerRegistrationEvent)
                    .Then(ctx =>
                    {
                        ctx.Saga.CorrelationId = ctx.Message.Id;
                        ctx.Saga.CreatedOn = ctx.Message.CreatedOn;
                        ctx.Saga.FirstName = ctx.Message.FirstName;
                        ctx.Saga.LastName = ctx.Message.LastName;
                        ctx.Saga.Email = ctx.Message.Email;
                        ctx.Saga.Phone = ctx.Message.Phone;
                        ctx.Saga.CreatedOn = DateTime.UtcNow;
                    })
                    .TransitionTo(this.PendingActivation)
                    .PublishAsync(context => context.Init<CustomerRegisteredEvent>(
                        new
                        {
                            Id = context.Saga.CorrelationId,
                            context.Message.FirstName,
                            context.Message.LastName,
                            context.Message.Email,
                            context.Message.Phone,
                            context.Saga.CreatedOn
                        })));

            this.During(this.PendingActivation,
                this.When(this.CustomerActivatedEvent)
                    .Then(ctx =>
                    {
                        ctx.Saga.ActivatedOn = DateTime.UtcNow;
                    })
                    .TransitionTo(this.Active));
        }

        public State None { get; set; }

        public State PendingActivation { get; set; }

        public State Active { get; set; }

        public State Inactive { get; set; }

        public Event<CustomerRegistrationEvent> CustomerRegistrationEvent { get; set; }

        public Event<CustomerActvationEvent> CustomerActivatedEvent { get; set; }
    }
}