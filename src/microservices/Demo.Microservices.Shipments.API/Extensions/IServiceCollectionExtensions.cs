using System.IO;
using System.Linq;
using Demo.Microservices.Shipments.API.OpenAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Demo.Microservices.Shipments.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void UseOpenApiSpecifications(this IServiceCollection services)
        {
            var xmlDocumentationPath = GetXmlDocumentationPath(services);

            services.AddSwaggerGen(swaggerGenerationOptions =>
            {
                swaggerGenerationOptions.EnableAnnotations();
                swaggerGenerationOptions.SwaggerDoc(OpenApiCategories.Shipments, CreateApiInformation("Shipments"));
                swaggerGenerationOptions.SwaggerDoc(OpenApiCategories.ShipmentManagement, CreateApiInformation("Shipment Management"));

                swaggerGenerationOptions.DescribeAllEnumsAsStrings();

                if (string.IsNullOrEmpty(xmlDocumentationPath) == false)
                {
                    swaggerGenerationOptions.IncludeXmlComments(xmlDocumentationPath);
                }
            });
        }

        private static Info CreateApiInformation(string microserviceName)
        {
            var openApiInformation = new Info
            {
                Contact = new Contact
                {
                    Name = "Codit",
                    Url = "https://codit.eu"
                },
                Title = $"Codito - {microserviceName} API",
                Description = $"{microserviceName} APIs of the Codito platform",
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