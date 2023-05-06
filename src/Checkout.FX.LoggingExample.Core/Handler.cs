using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Diagnostics.Abstractions;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation("Starting...");

            using (_logger.BeginScope(LoggingUtlities.AddAttribute(
                (Constants.Log.Properties.CurrencyPairs, new string[] { "USDGBP", "USDEUR" })
                )))
            {
                _logger.LogInformation("Processing...");
            }

            _logger.LogInformation("Completed");
            await Task.Yield();
        }
    }
}
