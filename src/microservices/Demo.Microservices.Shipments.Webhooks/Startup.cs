
using Demo.Microservices.Shipments.Data.Providers;
using Demo.Microservices.Shipments.Data.Repositories.InMemory;
using Demo.Microservices.Shipments.Data.Repositories.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Demo.Microservices.Shipments.Webhooks.Startup))]
namespace Demo.Microservices.Shipments.Webhooks
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IShipmentRepository, ShipmentsTableRepository>();
            builder.Services.AddScoped<TableStorageAccessor>();
        }
    }
}