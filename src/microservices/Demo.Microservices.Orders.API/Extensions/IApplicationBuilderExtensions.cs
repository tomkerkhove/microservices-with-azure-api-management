using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Demo.Microservices.Orders.API.Extensions
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
                swaggerUiOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Contoso - Orders API");

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