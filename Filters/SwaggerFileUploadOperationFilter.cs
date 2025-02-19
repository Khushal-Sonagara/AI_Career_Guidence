using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace AI_Career_Guidence.Filters
{
    public class SwaggerFileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // ✅ Only apply to POST requests in ResumePhotoController
            if (context.ApiDescription.HttpMethod != "POST" ||
                !context.ApiDescription.RelativePath.Contains("resume-photo"))
                return;

            // Check if the API has an IFormFile parameter
            bool isFileUpload = context.ApiDescription.ParameterDescriptions
                .Any(p => p.Type == typeof(IFormFile));

            if (!isFileUpload)
                return;

            // ✅ Ensure the request body includes both `file` and `userId`
            operation.RequestBody = new OpenApiRequestBody
            {
                Content = new Dictionary<string, OpenApiMediaType>
                {
                    ["multipart/form-data"] = new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                ["file"] = new OpenApiSchema
                                {
                                    Type = "string",
                                    Format = "binary"
                                },
                                ["userId"] = new OpenApiSchema
                                {
                                    Type = "integer",
                                    Format = "int32"
                                }
                            },
                            Required = new HashSet<string> { "file", "userId" } // ✅ Requires both fields
                        }
                    }
                }
            };
        }
    }
}
