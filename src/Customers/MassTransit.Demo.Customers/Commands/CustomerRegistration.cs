namespace MassTransit.Demo.Customers.Commands
{
    using FluentValidation;
    using System;

    public class CustomerRegistration
    {
        public class Command
        {
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string Phone { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                this.RuleFor(x => x.FirstName).NotEmpty();
                this.RuleFor(x => x.LastName).NotEmpty();
                this.RuleFor(x => x.Email).NotEmpty();
                this.RuleFor(x => x.Phone).NotEmpty();
            }
        }
    }
}