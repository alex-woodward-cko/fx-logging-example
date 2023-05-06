using System.Collections.Generic;
using System.Linq;
using MELT;

namespace Checkout.FX.LoggingExample.Core.UnitTests
{
    internal static class LogTestingExtensions
    {
        public static IEnumerable<LogEntry> GetLogs(this ITestLoggerFactory loggerFactory) =>
            loggerFactory.Sink.LogEntries;
        public static IEnumerable<LogEntry> GetLogs(this ITestLoggerFactory loggerFactory, string message) =>
            loggerFactory.GetLogs()
            .Where(log => log.Message.Contains(message));

        public static bool TryGetPropertyValue<TValue>(this LogEntry log, string key, out IEnumerable<TValue> properties)
            where TValue : class
        {
            properties = new List<TValue>();
            if (log == null) { return false; }

            properties = log.Scopes
                .SelectMany(scope => scope.Properties)
                .Where(property => property.Key == key)
                .Select(property => property.Value as TValue);

            return properties.Any();
        }


    }
}
