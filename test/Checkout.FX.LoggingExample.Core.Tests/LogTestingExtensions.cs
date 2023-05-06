using Serilog;
using Serilog.Sinks.InMemory;

namespace Checkout.FX.LoggingExample.Core.UnitTests
{
    internal static class LogTestingExtensions
    {
        public static ILogger Logger => new LoggerConfiguration()
                .WriteTo.InMemory()
                .CreateLogger();
    }
}
