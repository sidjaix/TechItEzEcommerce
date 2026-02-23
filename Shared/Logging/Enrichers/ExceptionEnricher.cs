using Serilog.Core;
using Serilog.Events;

namespace Logging.Enrichers
{
    public class ExceptionEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            if (logEvent.Exception == null)
            {
                return;
            }

            var exception = logEvent.Exception;
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("Exception", exception.Message));
            logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("StackTrace", exception.StackTrace));
        }
    }
}