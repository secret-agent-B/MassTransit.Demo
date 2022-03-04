namespace MassTransit.Demo.Customers.Commands
{
    using FluentValidation;

    public class CustomerActvation
    {
        public class Command
        {
            public Guid Id { get; set; }

            public string ActivationKey { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                this.RuleFor(x => x.Id).NotEmpty();
                this.RuleFor(x => x.ActivationKey).NotEmpty();
            }
        }
    }
}