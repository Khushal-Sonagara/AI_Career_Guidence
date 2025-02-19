using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace AI_Career_Guidence.Filters
{
    public class SwaggerResumeImagesUploadFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Ensure this only runs for POST methods in ResumeImagesController
            if (context.ApiDescription.HttpMethod != "POST" ||
                !context.ApiDescription.RelativePath.Contains("resume-images"))
                return;

            // Check if any parameter is an IFormFile
            bool isFileUpload = context.ApiDescription.ParameterDescriptions
                .Any(p => p.Type == typeof(IFormFile));

            if (!isFileUpload)
                return;

            // Apply file upload schema for Resume Images (ONLY `resumeId`, NOT `userId`)
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
                                ["resumeId"] = new OpenApiSchema
                                {
                                    Type = "integer",
                                    Format = "int32"
                                }
                            },
                            Required = new HashSet<string> { "file", "resumeId" } // ✅ Only requires `resumeId`
                        }
                    }
                }
            };
        }
    }
}
