using System;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Diagnostics.Abstractions;
using Serilog;

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
        public Handler(
            ILogger logger, IMetrics metrics)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _logger.ForContext<Handler>();
            _metrics = metrics;
        }

        /// <inheritdoc />
        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            _logger.Information("The fx-logging-example Lambda starts");
            _logger.Information("Hello world!");
            _logger.Information("The fx-logging-example Lambda finishes");
            await Task.Yield();
        }
    }
}
