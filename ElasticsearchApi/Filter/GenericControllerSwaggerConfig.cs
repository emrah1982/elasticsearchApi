using ElasticsearchApi.Model;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ElasticsearchApi.Filter
{
    public class GenericControllerSwaggerConfig : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var genericControllers = context.ApiDescriptions
                .Where(desc => desc.ActionDescriptor.EndpointMetadata
                               .OfType<ControllerModel>()
                               .Any(c => c.ControllerType.IsGenericType))
                .ToList();

            foreach (var desc in genericControllers)
            {
                var controllerType = desc.ActionDescriptor.EndpointMetadata
                    .OfType<ControllerModel>()
                    .First()
                    .ControllerType;

                var genericType = controllerType.GenericTypeArguments[0];
                var path = $"/api/{controllerType.Name.Replace("`1", string.Empty)}/{genericType.Name}";

                if (!swaggerDoc.Paths.ContainsKey(path))
                    swaggerDoc.Paths.Add(path, new OpenApiPathItem { /* Configure as needed */ });
            }
        }
    }
}
