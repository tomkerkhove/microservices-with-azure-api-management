using Demo.Microservices.Orders.API.Data.Providers;
using Demo.Microservices.Orders.API.Extensions;
using Demo.Microservices.Orders.API.Managers;
using Demo.Microservices.Orders.API.Repositories.InMemory;
using Demo.Microservices.Orders.API.Repositories.Interfaces;
using Demo.Microservices.Orders.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using IShipmentService = Demo.Microservices.Orders.API.Services.Interfaces.IShipmentService;

namespace Demo.Microservices.Orders.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                });

            services.AddScoped<IShipmentService, ShipmentService>();
            services.AddScoped<IOrderRepository, OrderTableRepository>();
            services.AddScoped<OrderManager>();
            services.AddScoped<TableStorageAccessor>();

            services.UseOpenApiSpecifications();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseOpenApiUi();
        }
    }
}
