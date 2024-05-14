using ElasticsearchApi.Model;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace ElasticsearchApi.Filter
{
    public class GenericControllerDocumentFilter : IDocumentFilter
    //public class GenericControllerDocumentFilter : SchemaFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var types = new Type[] { typeof(CompanyDTO), typeof(EmployeeDTO) }; // List all types you want to handle
            foreach (Type type in types)
            {
                //string controllerName = type.Name + "Controller";
                string controllerName = $"Generic{type.Name}Controller";
                string route = $"/api/{controllerName.ToLower()}";

                if (!swaggerDoc.Paths.ContainsKey(route))
                {
                    swaggerDoc.Paths.Add(route, new OpenApiPathItem());
                }

                var pathItem = swaggerDoc.Paths[route];

                // GET Operation
                if (!pathItem.Operations.ContainsKey(OperationType.Get))
                {
                    pathItem.Operations[OperationType.Get] = new OpenApiOperation
                    {
                        Tags = new List<OpenApiTag> { new OpenApiTag { Name = controllerName } },
                        OperationId = $"Get{type.Name}",
                        Parameters = new List<OpenApiParameter>
                    {
                        new OpenApiParameter { Name = "id", In = ParameterLocation.Path, Required = true, Schema = new OpenApiSchema { Type = "string" } }
                    },
                        Responses = new OpenApiResponses
                        {
                            ["200"] = new OpenApiResponse { Description = "Success" },
                            ["404"] = new OpenApiResponse { Description = "Not Found" }
                        }
                    };

                }

                if (!pathItem.Operations.ContainsKey(OperationType.Post))
                {
                    // POST Operation
                    pathItem.Operations[OperationType.Post] = new OpenApiOperation
                    {
                        Tags = new List<OpenApiTag> { new OpenApiTag { Name = controllerName } },
                        OperationId = $"Create{type.Name}",
                        Parameters = new List<OpenApiParameter>(),
                        RequestBody = new OpenApiRequestBody
                        {
                            Content = new Dictionary<string, OpenApiMediaType>
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "object",
                                        AdditionalProperties = new OpenApiSchema
                                        {
                                            Type = "string"
                                        }
                                    }
                                }
                            }
                        },
                        Responses = new OpenApiResponses
                        {
                            ["201"] = new OpenApiResponse { Description = "Created" }
                        }
                    };
                }
                if (!pathItem.Operations.ContainsKey(OperationType.Put))
                {
                    // PUT Operation
                    pathItem.Operations[OperationType.Put] = new OpenApiOperation
                    {
                        Tags = new List<OpenApiTag> { new OpenApiTag { Name = controllerName } },
                        OperationId = $"Update{type.Name}",
                        Parameters = new List<OpenApiParameter>
                    {
                        new OpenApiParameter { Name = "id", In = ParameterLocation.Path, Required = true, Schema = new OpenApiSchema { Type = "string" } }
                    },
                        RequestBody = new OpenApiRequestBody
                        {
                            Content = new Dictionary<string, OpenApiMediaType>
                            {
                                ["application/json"] = new OpenApiMediaType
                                {
                                    Schema = new OpenApiSchema
                                    {
                                        Type = "object",
                                        AdditionalProperties = new OpenApiSchema
                                        {
                                            Type = "string"
                                        }
                                    }
                                }
                            }
                        },
                        Responses = new OpenApiResponses
                        {
                            ["204"] = new OpenApiResponse { Description = "Updated" }
                        }
                    };
                }
                if (!pathItem.Operations.ContainsKey(OperationType.Delete))
                {
                    // DELETE Operation
                    pathItem.Operations[OperationType.Delete] = new OpenApiOperation
                    {
                        Tags = new List<OpenApiTag> { new OpenApiTag { Name = controllerName } },
                        OperationId = $"Delete{type.Name}",
                        Parameters = new List<OpenApiParameter>
                    {
                        new OpenApiParameter { Name = "id", In = ParameterLocation.Path, Required = true, Schema = new OpenApiSchema { Type = "string" } }
                    },
                        Responses = new OpenApiResponses
                        {
                            ["204"] = new OpenApiResponse { Description = "Deleted" },
                            ["404"] = new OpenApiResponse { Description = "Not Found" }
                        }
                    };
                }
            }
        }
    }
}

//using ElasticsearchApi.Model;
//using Microsoft.OpenApi.Models;
//using Swashbuckle.AspNetCore.SwaggerGen;

//namespace ElasticsearchApi.Filter
//{
//    public class GenericControllerDocumentFilter : IDocumentFilter
//    {
//        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
//        {
//            var types = new Type[] { typeof(CompanyDTO), typeof(EmployeeDTO) }; // List all types you want to handle
//            //var types = new Type[] { typeof(CompanyDTO) }; // List all types you want to handle
//            foreach (Type type in types)
//            {
//                string controllerName = type.Name + "Controller";
//                string route = $"/api/{controllerName.ToLower()}";

//                swaggerDoc.Paths.Add(route, new OpenApiPathItem
//                {
//                    Operations = new Dictionary<OperationType, OpenApiOperation>
//                    {
//                        [OperationType.Get] = new OpenApiOperation
//                        {
//                            Tags = new List<OpenApiTag> { new OpenApiTag { Name = controllerName } },
//                            OperationId = $"Get{type.Name}",
//                            Parameters = new List<OpenApiParameter>
//                        {
//                            new OpenApiParameter { Name = "id", In = ParameterLocation.Path, Required = true, Schema = new OpenApiSchema { Type = "string" } }
//                        },
//                            Responses = new OpenApiResponses
//                            {
//                                ["200"] = new OpenApiResponse { Description = "Success" },
//                                ["404"] = new OpenApiResponse { Description = "Not Found" }
//                            }
//                        },
//                        // Add other operations similarly
//                    }
//                });
//            }
//        }
//    }
//}
