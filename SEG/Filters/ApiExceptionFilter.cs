using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SEG.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, $"Error 500: {context.Exception.Message}");
        context.Result = new ObjectResult($"Error 500: {context.Exception.Message}")
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };
    }
}
