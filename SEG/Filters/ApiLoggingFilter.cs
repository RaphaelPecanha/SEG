using Microsoft.AspNetCore.Mvc.Filters;

namespace SEG.Filters;

public class ApiLoggingFilter : IActionFilter
{
    private readonly ILogger<ApiLoggingFilter> _logger;

    public ApiLoggingFilter(ILogger<ApiLoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        //Informação quando terminar de executar a Action
        _logger.LogInformation("## Finalizando Execução da action");
        _logger.LogInformation($"{DateTime.Now.ToShortTimeString}");
        _logger.LogInformation($"Status code: {context.HttpContext.Response.StatusCode}");
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        //Informação quando for executar o Action
        _logger.LogInformation("## EXECUTANDO");
        _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}");
        _logger.LogInformation($"Model State: {context.ModelState.IsValid}");
        _logger.LogInformation("########################");
    }
}
