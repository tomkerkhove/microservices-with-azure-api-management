﻿using Demo.Monolith.API.OpenAPI;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Demo.Monolith.API.Extensions
{
    /// <summary>
    /// Provides an Application Builder extension for the Swagger/OpenAPI integration
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Add OpenAPI specification generation
        /// </summary>
        /// <param name="app">The ApplicationBuilder instance</param>
        /// <param name="provider">The APIVersionDescriptionProvider</param>
        public static void UseOpenApiUi(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(swaggerUiOptions =>
            {
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{OpenApiCategories.Monolith}/swagger.json", "Contoso - Monolith API");
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{OpenApiCategories.Orders}/swagger.json", "Contoso - Orders API");
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{OpenApiCategories.Products}/swagger.json", "Contoso - Products API");
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{OpenApiCategories.Shipments}/swagger.json", "Contoso - Shipments API");
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{OpenApiCategories.ShipmentWebhook}/swagger.json", "Contoso - Shipment Webhook API");

                swaggerUiOptions.RoutePrefix = "api/docs";
                swaggerUiOptions.DisplayOperationId();
                swaggerUiOptions.EnableDeepLinking();
                swaggerUiOptions.DocExpansion(DocExpansion.List);
                swaggerUiOptions.DisplayRequestDuration();
                swaggerUiOptions.EnableFilter();
            });
        }
    }
}