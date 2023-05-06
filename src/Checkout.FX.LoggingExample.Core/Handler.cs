using System;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Diagnostics.Abstractions;
using Serilog;
using Serilog.Context;

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

            using (LogContext.PushProperty(Constants.Log.Properties.CurrencyPairs, new string[] { "USDGBP", "USDEUR" }))
            {
                _logger.Information("Processing...");
                await Task.Delay(1000);
            }

            _logger.Information("Completed");
            await Task.Yield();
        }
    }
}
