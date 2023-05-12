using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Demo.Microservices.Orders.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void UseOpenApiSpecifications(this IServiceCollection services)
        {
            var xmlDocumentationPath = GetXmlDocumentationPath(services);
            var openApiInformation = new Info
            {
                Contact = new Contact
                {
                    Name = "Contoso",
                    Url = "https://codit.eu"
                },
                Title = $"Contoso - Orders API",
                Description = $"Orders APIs of the Contoso platform",
                Version = "v1"
            };

            services.AddSwaggerGen(swaggerGenerationOptions =>
            {
                swaggerGenerationOptions.EnableAnnotations();
                swaggerGenerationOptions.SwaggerDoc("v1", openApiInformation);

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
                    Name = "Contoso",
                    Url = "https://codit.eu"
                },
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