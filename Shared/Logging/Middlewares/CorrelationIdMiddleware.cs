using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Serilog.Context;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Logging.Middlewares;

[ExcludeFromCodeCoverage]
public class CorrelationIdMiddleware
{
    private readonly RequestDelegate _next;
    private readonly CorrelationIdOptions _options;

    public CorrelationIdMiddleware(RequestDelegate next, IOptions<CorrelationIdOptions> options)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var correlationId = GetOrSetCorrelationId(httpContext);

        using (LogContext.PushProperty("UserTKID", "24200"))
        using (LogContext.PushProperty(CorrelationIdOptions.CorrelationId, correlationId))
        {
            await _next(httpContext);
        }
    }

    private string GetOrSetCorrelationId(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.TryGetValue(_options.HeaderName, out StringValues correlationIdValues) && !StringValues.IsNullOrEmpty(correlationIdValues))
        {
            return correlationIdValues.ToString();
        }

        var newCorrelationId = Guid.NewGuid().ToString();
        httpContext.Request.Headers.Append(_options.HeaderName, newCorrelationId);
        return newCorrelationId;
    }
}
