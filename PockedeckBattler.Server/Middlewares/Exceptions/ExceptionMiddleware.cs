using Microsoft.AspNetCore.Mvc;

namespace PockedeckBattler.Server.Middlewares.Exceptions;

public class ExceptionMiddleware : IMiddleware
{
    readonly IWebApiExceptionHandler[] _handlers;
    readonly ILogger<ExceptionMiddleware> _logger;
    readonly IProblemDetailsService _problemDetailsService;

    public ExceptionMiddleware(IProblemDetailsService problemDetailsService, IEnumerable<IWebApiExceptionHandler> handlers, ILogger<ExceptionMiddleware> logger)
    {
        _problemDetailsService = problemDetailsService;
        _logger = logger;
        _handlers = handlers.ToArray();
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            ProblemDetails? problemDetails = null;

            if (e is IWebApiException webApiException)
            {
                problemDetails = new ProblemDetails
                {
                    Title = webApiException.Title,
                    Detail = webApiException.Message ?? "Unknown error",
                    Status = (int)webApiException.Status
                };
            }
            foreach (IWebApiExceptionHandler handler in _handlers)
            {
                problemDetails = handler.Handle(e);
                if (problemDetails != null)
                {
                    break;
                }
            }

            if (problemDetails != null)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("Cannot send problem details: response has already started");
                }
                else
                {
                    context.Response.Clear();
                    context.Response.StatusCode = problemDetails.Status ?? 500;
                    await _problemDetailsService.WriteAsync(new ProblemDetailsContext { HttpContext = context, ProblemDetails = problemDetails });

                    _logger.LogError(e, "An error occurred while executing the request");

                    return;
                }
            }

            throw;
        }
    }
}
