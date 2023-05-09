using System;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Diagnostics.Abstractions;
using Serilog;
using Serilog.Events;

namespace Checkout.FX.LoggingExample.Core
{
    /// <inheritdoc />
    public class Handler : IHandler
    {
        private readonly ILogger _logger;
        private readonly IMetrics _metrics;

        /// <summary>
        /// Constructs Handler class
        /// </summary>
        public Handler(ILogger logger, IMetrics metrics)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
        }

        /// <inheritdoc />
        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Starting...");

            var pairs = new string[] { "USDGBP", "USDEUR" };

            _logger
                .ForContext(LogEventLevel.Information, Constants.Log.Properties.CurrencyPairs, pairs, true)
                .Information("Processing...");

            await Task.Delay(1000);

            _logger.Information("Completed");
            await Task.Yield();
        }
    }
}
