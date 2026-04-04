using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace ApiCommon.Handlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;
    private readonly IProblemDetailsService _problemDetailsService;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        // Extract the exact OpenTelemetry Trace ID
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

        // Log the full exception to Serilog/Seq. 
        _logger.LogError(
            exception,
            "An unhandled exception occurred during request to {Path}. TraceId: {TraceId}",
            httpContext.Request.Path,
            traceId);

        // Write the secure response back to the client without exposing sensitive details, but include the Trace ID for correlation
        httpContext.Response.StatusCode = exception switch
        {
            ApplicationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Title = "An unexpected server error occurred.",
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
            Detail = "The system encountered an error. Please contact support with the provided Trace ID.",
            Status = httpContext.Response.StatusCode,
            Instance = httpContext.Request.Path
        };

        // Inject the W3C TraceId into the JSON response for the frontend developer/user
        problemDetails.Extensions.Add("traceId", traceId);

        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = problemDetails
        });

    }
}
