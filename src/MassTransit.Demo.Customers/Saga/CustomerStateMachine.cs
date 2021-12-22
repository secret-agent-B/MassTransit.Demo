namespace MassTransit.Demo.Customers.Saga
{
    using Automatonymous;
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
            this.Event(() => this.CustomerActivatedEvent, c => c.CorrelateBy((instance, context) => instance.CorrelationId == context.Message.Id));

            this.Initially(
                this.When(this.CustomerRegistrationEvent)
                    .Then(ctx =>
                    {
                        ctx.Instance.CorrelationId = ctx.Data.Id;
                        ctx.Instance.CreatedOn = ctx.Data.CreatedOn;
                        ctx.Instance.FirstName = ctx.Data.FirstName;
                        ctx.Instance.LastName = ctx.Data.LastName;
                        ctx.Instance.Email = ctx.Data.Email;
                        ctx.Instance.Phone = ctx.Data.Phone;
                        ctx.Instance.CreatedOn = DateTime.UtcNow;
                    })
                    .TransitionTo(this.PendingActivation)
                    .PublishAsync(context => context.Init<CustomerRegisteredEvent>(
                        new
                        {
                            Id = context.Instance.CorrelationId,
                            context.Data.FirstName,
                            context.Data.LastName,
                            context.Data.Email,
                            context.Data.Phone,
                            context.Instance.CreatedOn
                        })));

            this.During(this.PendingActivation,
                this.When(this.CustomerActivatedEvent)
                    .Then(ctx =>
                    {
                        ctx.Instance.ActivatedOn = DateTime.UtcNow;
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