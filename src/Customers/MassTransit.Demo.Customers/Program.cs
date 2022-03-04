using FluentValidation;
using MassTransit;
using MassTransit.Demo.Communication.Extensions;
using MassTransit.Demo.Customers.Saga;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder
    .Services
    .AddMassTransitMiddleware((serviceCollectionBusConfig, configuration) =>
    {
        serviceCollectionBusConfig
            .AddSagaStateMachine<CustomerStateMachine, Customer>()
            .MongoDbRepository( 
                cfg =>
                {
                    cfg.Connection = "mongodb://masstransit.demo.mongo";
                    cfg.DatabaseName = "customersdb";
                    cfg.CollectionName = "customers";

                    BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
                });
    })
    .AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Host
    .AddConfiguration()
    .AddSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

public partial class Program
{ }