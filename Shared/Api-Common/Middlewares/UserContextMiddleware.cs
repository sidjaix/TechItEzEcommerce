using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;

namespace ApiCommon.Middlewares;

[ExcludeFromCodeCoverage]
public class UserContextMiddleware
{
    private readonly RequestDelegate _next;

    public UserContextMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        var userId = httpContext.User?.FindFirst(JwtRegisteredClaimNames.Sub)?.Value ?? "anonymous";

        // Push to Serilog so all logs in Seq have the UserId attached
        using (LogContext.PushProperty("UserId", userId))
        {
            // OPTIONAL BUT POWERFUL: Add the UserId to the OpenTelemetry Trace!
            // This allows you to search Jaeger for traces by a specific user.
            var activity = Activity.Current;
            if (activity != null && userId != "anonymous")
            {
                activity.SetTag("user.id", userId);
            }
            await _next(httpContext);
        }
    }
}
