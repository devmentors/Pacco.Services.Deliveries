using System;
using System.Linq;
using Convey;
using Convey.CQRS.Queries;
using Convey.Discovery.Consul;
using Convey.HTTP;
using Convey.LoadBalancing.Fabio;
using Convey.MessageBrokers.CQRS;
using Convey.MessageBrokers.RabbitMQ;
using Convey.Metrics.AppMetrics;
using Convey.Persistence.MongoDB;
using Convey.Persistence.Redis;
using Convey.Tracing.Jaeger;
using Convey.Tracing.Jaeger.RabbitMQ;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pacco.Services.Deliveries.Application;
using Pacco.Services.Deliveries.Application.Commands;
using Pacco.Services.Deliveries.Application.Services;
using Pacco.Services.Deliveries.Core.Repositories;
using Pacco.Services.Deliveries.Infrastructure.Contexts;
using Pacco.Services.Deliveries.Infrastructure.Exceptions;
using Pacco.Services.Deliveries.Infrastructure.Logging;
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
            builder.Services.AddTransient<IAppContextFactory, AppContextFactory>();
            builder.Services.AddTransient(ctx => ctx.GetRequiredService<IAppContextFactory>().Create());

            return builder
                .AddQueryHandlers()
                .AddInMemoryQueryDispatcher()
                .AddHttpClient()
                .AddConsul()
                .AddFabio()
                .AddRabbitMq(plugins: p => p.AddJaegerRabbitMqPlugin())
                .AddExceptionToMessageMapper<ExceptionToMessageMapper>()
                .AddMongo()
                .AddRedis()
                .AddMetrics()
                .AddJaeger()
                .AddHandlersLogging()
                .AddMongoRepository<DeliveryDocument, Guid>("Deliveries");
        }

        public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app)
        {
            app.UseErrorHandler()
                .UseJaeger()
                .UseInitializers()
                .UsePublicContracts<ContractAttribute>()
                .UseMetrics()
                .UseRabbitMq()
                .SubscribeCommand<StartDelivery>()
                .SubscribeCommand<CompleteDelivery>()
                .SubscribeCommand<FailDelivery>()
                .SubscribeCommand<AddDeliveryRegistration>();

            return app;
        }
        
        internal static CorrelationContext GetCorrelationContext(this IHttpContextAccessor accessor)
            => accessor.HttpContext.Request.Headers.TryGetValue("Correlation-Context", out var json)
                ? JsonConvert.DeserializeObject<CorrelationContext>(json.FirstOrDefault())
                : null;
    }
}