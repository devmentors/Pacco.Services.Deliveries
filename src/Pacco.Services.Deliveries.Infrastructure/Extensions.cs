using System;
using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Persistence.MongoDB;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacco.Services.Deliveries.Application;
using Pacco.Services.Deliveries.Application.Commands;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Repositories;
using Pacco.Services.Deliveries.Infrastructure.Exceptions;
using Pacco.Services.Deliveries.Infrastructure.Mongo.Documents;
using Pacco.Services.Deliveries.Infrastructure.Mongo.Repositories;
using Pacco.Services.Deliveries.Infrastructure.Services;

namespace Pacco.Services.Deliveries.Infrastructure
{
    public static class Extensions
    {
        public static IConveyBuilder AddInfrastructure(this IConveyBuilder builder)
        {
            builder.Services.AddSingleton<IEventMapper, EventMapper>();
            builder.Services.AddTransient<IMessageBroker, MessageBroker>();
            builder.Services.AddTransient<IDeliveriesRepository, DeliveriesMongoRepository>();
            builder.Services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            
            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq()
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddMongoRepository<DeliveryDocument, Guid>("Deliveries");
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UsePublicContracts<ContractAttribute>()
                .UseInitializers()
                .UseConsul()
                .UseRabbitMq()
                .SubscribeCommand<StartDelivery>()
                .SubscribeCommand<CompleteDelivery>()
                .SubscribeCommand<FailDelivery>()
                .SubscribeCommand<AddDeliveryRegistration>();
            
            return app;
        }
    }
}