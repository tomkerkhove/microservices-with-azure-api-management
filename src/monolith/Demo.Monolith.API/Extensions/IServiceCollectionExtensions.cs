using System;
using System.IO;
using System.Linq;
using Demo.Monolith.API.OpenAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Demo.Monolith.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void UseOpenApiSpecifications(this IServiceCollection services)
        {
            var xmlDocumentationPath = GetXmlDocumentationPath(services);

            services.AddSwaggerGen(swaggerGenerationOptions =>
            {
                swaggerGenerationOptions.EnableAnnotations();
                swaggerGenerationOptions.SwaggerDoc(OpenApiCategories.Monolith, CreateApiInformation("Monolith"));
                swaggerGenerationOptions.SwaggerDoc(OpenApiCategories.Orders, CreateApiInformation("Orders"));
                swaggerGenerationOptions.SwaggerDoc(OpenApiCategories.Products, CreateApiInformation("Products"));
                swaggerGenerationOptions.SwaggerDoc(OpenApiCategories.Shipments, CreateApiInformation("Shipments"));
                swaggerGenerationOptions.SwaggerDoc(OpenApiCategories.ShipmentWebhook, CreateApiInformation("Shipment Webhook"));
                swaggerGenerationOptions.DocInclusionPredicate(IncludeAllOperationsInMonolithApi);

                if (string.IsNullOrEmpty(xmlDocumentationPath) == false)
                {
                    swaggerGenerationOptions.IncludeXmlComments(xmlDocumentationPath);
                }
            });
        }

        private static bool IncludeAllOperationsInMonolithApi(string docName, ApiDescription apiDesc)
        {
            if (docName.Equals(OpenApiCategories.Monolith, StringComparison.InvariantCultureIgnoreCase)
            || apiDesc.GroupName == null)
            {
                return true;
            }

            return apiDesc.GroupName.Equals(docName, StringComparison.InvariantCultureIgnoreCase);
        }

        private static OpenApiInfo CreateApiInformation(string microserviceName)
        {
            var openApiInformation = new OpenApiInfo
            {
                Title = $"Contoso - {microserviceName} API",
                Description = $"{microserviceName} APIs of the Contoso platform",
                Version = "v1"
            };
            return openApiInformation;
        }

        private static string GetXmlDocumentationPath(IServiceCollection services)
        {
            var hostingEnvironment = services.FirstOrDefault(service => service.ServiceType == typeof(IHostingEnvironment));
            if (hostingEnvironment == null)
            {
                return string.Empty;
            }

            var contentRootPath = ((IHostingEnvironment)hostingEnvironment.ImplementationInstance).ContentRootPath;
            var xmlDocumentationPath = $"{contentRootPath}/Docs/Open-Api.xml";

            return File.Exists(xmlDocumentationPath) ? xmlDocumentationPath : string.Empty;
        }

    }
}