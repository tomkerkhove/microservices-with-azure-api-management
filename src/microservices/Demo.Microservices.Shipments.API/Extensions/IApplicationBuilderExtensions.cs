using Demo.Microservices.Shipments.API.OpenAPI;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Demo.Microservices.Shipments.API.Extensions
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
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{OpenApiCategories.Shipments}/swagger.json", "Codito - Shipments API");
                swaggerUiOptions.SwaggerEndpoint($"/swagger/{OpenApiCategories.ShipmentManagement}/swagger.json", "Codito - Shipment Management API");

                swaggerUiOptions.DisplayOperationId();
                swaggerUiOptions.EnableDeepLinking();
                swaggerUiOptions.DocExpansion(DocExpansion.List);
                swaggerUiOptions.DisplayRequestDuration();
                swaggerUiOptions.EnableFilter();
            });
        }
    }
}