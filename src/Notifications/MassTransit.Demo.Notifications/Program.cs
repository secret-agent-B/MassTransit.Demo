// See https://aka.ms/new-console-template for more information

using MassTransit.Demo.Communication.Extensions;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder();

builder
    .AddConfiguration()
    .ConfigureServices(services =>
    {
        services.AddMassTransitMiddleware((serviceCollectionBusConfig, config) =>
        {
            serviceCollectionBusConfig.ConfigureBus<Program>(config);
        });
    })
    .AddSerilog();

var host = builder.Build();

await host.RunAsync();

public partial class Program
{ }