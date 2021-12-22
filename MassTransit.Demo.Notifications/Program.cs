// See https://aka.ms/new-console-template for more information

using MassTransit.Demo.Communication.Extensions;
using MassTransit.Demo.Notifications.Consumers;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder();

builder
    .AddConfiguration()
    .ConfigureServices(services =>
    {
        services.AddMassTransitMiddleware((serviceCollectionBusConfig, configuration) =>
        {
            serviceCollectionBusConfig.AddConsumer<CustomerRegisteredConsumer>();
        });
    })
    .AddSerilog();

var host = builder.Build();

await host.RunAsync();