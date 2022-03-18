namespace MassTransit.Demo.Shared.Extensions
{
using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollection
    {
        public static IServiceCollection ConfigureDemoServices<T>(this IServiceCollection services)
            where T : class
        {
            services.AddValidatorsFromAssembly(typeof(T).Assembly);
            return services;
        }
    }
}
