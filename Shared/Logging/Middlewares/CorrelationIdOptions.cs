using System;
using System.Diagnostics.CodeAnalysis;

namespace Logging.Middlewares;

[ExcludeFromCodeCoverage]
public class CorrelationIdOptions
{
    public const string CorrelationId = "CorrelationId";

    public string HeaderName { get; set; } = "X-Correlation-ID";
}
