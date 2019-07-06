using System.Threading.Tasks;
using Convey;
using Convey.Logging;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pacco.Services.Deliveries.Application;
using Pacco.Services.Deliveries.Application.Commands;
using Pacco.Services.Deliveries.Application.DTO;
using Pacco.Services.Deliveries.Application.Queries;
using Pacco.Services.Deliveries.Infrastructure;

namespace Pacco.Services.Deliveries
{
    public class Program
    {
        public static async Task Main(string[] args)
            => await WebHost.CreateDefaultBuilder(args)
                .ConfigureServices(services => services
                    .AddConvey()
                    .AddWebApi()
                    .AddApplication()
                    .AddInfrastructure()
                    .Build())
                .Configure(app => app
                    .UseInfrastructure()
                    .UseDispatcherEndpoints(endpoints => endpoints
                        .Get("", ctx => ctx.Response.WriteAsync("Welcome to Pacco Deliveries Service!"))
                        .Get<GetDelivery, DeliveryDto>("deliveries/{orderId}")
                        .Post<StartDelivery>("deliveries",
                            afterDispatch: (cmd, ctx) => ctx.Response.Created($"deliveries/{cmd.OrderId}"))
                        .Post<FailDelivery>("deliveries/{id}/fail")
                        .Post<CompleteDelivery>("deliveries/{id}/complete")
                        .Post<AddDeliveryRegistration>("deliveries/{id}/registrations")))
                .UseLogging()
                .Build()
                .RunAsync();
    }
}
