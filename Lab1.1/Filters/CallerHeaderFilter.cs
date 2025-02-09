using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lab1._1.Filters;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

public class CallerHeaderFilter : ActionFilterAttribute
{
    private readonly ILogger<CallerHeaderFilter> _logger;

    public CallerHeaderFilter(ILogger<CallerHeaderFilter> logger)
    {
        _logger = logger;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var callerHeader = context.HttpContext.Request.Headers["caller"];

        if (string.IsNullOrEmpty(callerHeader) || callerHeader == "Unknown")
        {
            _logger.LogWarning("Invalid caller header. Request canceled.");
            context.Result = new BadRequestObjectResult("Invalid caller header. Request canceled.");
        }

        base.OnActionExecuting(context);
    }
}

public class AddHeadersOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
       
        operation.Parameters ??= new List<OpenApiParameter>();
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "caller",
            In = ParameterLocation.Header,
            Description = "Your custom header description",
            Required = false,
            Schema = new OpenApiSchema { Type = "string" }
        });
    }
}