using System.Collections.Generic;
using System.Threading.Tasks;
using Convey;
using Convey.CQRS.Commands;
using Convey.CQRS.Events;
using Convey.CQRS.Queries;
using Convey.Logging;
using Convey.WebApi;
using Convey.WebApi.CQRS;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Pacco.Services.Deliveries.Application;
using Pacco.Services.Deliveries.Application.Commands;
using Pacco.Services.Deliveries.Infrastructure;

namespace Pacco.Services.Deliveries
{
    public class Program
    {
        public static async Task Main(string[] args) => await WebHost.CreateDefaultBuilder(args)
            .ConfigureServices(services => services
                .AddConvey()
                .AddWebApi()
                .AddApplication()
                .AddInfrastructure()
                .Build())
            .Configure(app => app
                .UseInfrastructure()
               )
            .UseLogging()
            .Build()
            .RunAsync();
    }
}
