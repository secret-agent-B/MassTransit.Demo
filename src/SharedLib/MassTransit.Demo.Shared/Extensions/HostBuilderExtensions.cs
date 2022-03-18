namespace MassTransit.Demo.Communication.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using System.Reflection;

    public static class HostBuilderExtensions
    {
        public static IHostBuilder AddSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder
                .UseSerilog()
                .ConfigureLogging((ctx, lb) =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(ctx.Configuration)
                        .CreateLogger()
                        .ForContext(
                            "Service",
                            Assembly.GetEntryAssembly()?.GetName().Name
                                ?? Assembly.GetExecutingAssembly().GetName().Name);
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton(Log.Logger);
                });

            return hostBuilder;
        }

        public static IHostBuilder AddConfiguration(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration(cb =>
                 {
                     var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                     cb.SetBasePath(Directory.GetCurrentDirectory())
                        .AddEnvironmentVariables()
                        .AddJsonFile($"configs/bus-amqp.{environment}.json")
                        .AddJsonFile($"configs/logging.{environment}.json");
                 });

            return hostBuilder;
        }
    }
}