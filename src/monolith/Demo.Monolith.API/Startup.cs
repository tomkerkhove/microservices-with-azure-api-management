﻿using Demo.Monolith.API.Data.Providers;
using Demo.Monolith.API.Extensions;
using Demo.Monolith.API.Managers;
using Demo.Monolith.API.Repositories.InMemory;
using Demo.Monolith.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Demo.Monolith.API
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
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

                });

            services.AddScoped<IShipmentRepository, ShipmentsTableRepository>();
            services.AddScoped<IProductRepository, ProductTableRepository>();
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

            app.UseRouting()
               .UseEndpoints(o => o.MapControllers())
               .UseOpenApiUi();
        }
    }
}
