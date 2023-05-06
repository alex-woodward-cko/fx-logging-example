using System.Threading;
using System.Threading.Tasks;
using Checkout.Diagnostics.Abstractions;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.InMemory;
using Serilog.Sinks.InMemory.Assertions;

namespace Checkout.FX.LoggingExample.Core.Tests
{
    public class HandlerTests
    {
        private IHandler _handler;

        private ILogger _logger;
        private Mock<IMetrics> _mockMetrics;

        public HandlerTests()
        {
            _logger = new LoggerConfiguration()
                .WriteTo.InMemory()
                .CreateLogger();

            _mockMetrics = new Mock<IMetrics>();
            _handler = new Handler(_logger, _mockMetrics.Object);
        }

        [Fact]
        public void DoesExists()
        {
            _handler.Should().NotBeNull();
        }

        [Fact]
        public async Task Handle_Successfully()
        {
            // Arrange
            var pairs = new string[] { "USDGBP", "USDEUR" };

            // Act
            await _handler.HandleAsync(CancellationToken.None);

            // Assert
            VerifyInfoLog_Starting();
            VerifyInfoLog_Processing(pairs);
            VerifyInfoLog_Completed();
        }

        private void VerifyInfoLog_Starting()
        {
            InMemorySink.Instance
                .Should()
                .HaveMessage("Starting...")
                .WithLevel(LogEventLevel.Information)
                .Appearing().Once();
        }

        private void VerifyInfoLog_Processing(string[] expectedCurrencyPairs)
        {
            InMemorySink.Instance
                .Should()
                .HaveMessage("Processing...")
                .WithLevel(LogEventLevel.Information)
                .Appearing().Once()
                .WithProperty("currency-pairs");
                // .WithValue(expectedCurrencyPairs);
        }

        private void VerifyInfoLog_Completed()
        {
            InMemorySink.Instance
                .Should()
                .HaveMessage("Completed")
                .WithLevel(LogEventLevel.Information)
                .Appearing().Once();
        }
    }
}
