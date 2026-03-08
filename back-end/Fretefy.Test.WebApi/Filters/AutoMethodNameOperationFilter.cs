using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fretefy.Test.WebApi.Filters
{
    public class AutoMethodNameOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (string.IsNullOrWhiteSpace(operation.Summary))
            {
                operation.Summary = context.MethodInfo.Name;
            }
        }
    }
}
