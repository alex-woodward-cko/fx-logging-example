using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Diagnostics.Abstractions;
using Checkout.FX.LoggingExample.Core.UnitTests;
using MELT;
using Microsoft.Extensions.Logging;

namespace Checkout.FX.LoggingExample.Core.Tests
{
    public class HandlerTests
    {
        private IHandler _handler;

        private ITestLoggerFactory _loggerFactory;
        private ILogger _logger;
        private Mock<IMetrics> _mockMetrics;

        public HandlerTests()
        {
            _loggerFactory = TestLoggerFactory.Create();
            _logger = _loggerFactory.CreateLogger<Handler>();
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
            var log = _loggerFactory.GetLogs("Starting...").FirstOrDefault();
            log.Should().NotBeNull();
            log.LogLevel.Should().Be(LogLevel.Information);
        }

        private void VerifyInfoLog_Processing(string[] expectedCurrencyPairs)
        {
            var log = _loggerFactory.GetLogs("Processing...").FirstOrDefault();
            log.Should().NotBeNull();
            log.LogLevel.Should().Be(LogLevel.Information);
            log.TryGetPropertyValue<string[]>("CurrencyPairs", out var properties)
                .Should().BeTrue();
            properties.FirstOrDefault()
                .Should().NotBeNullOrEmpty()
                .And.Contain(expectedCurrencyPairs);
        }

        private void VerifyInfoLog_Completed()
        {
            var log = _loggerFactory.GetLogs("Completed").FirstOrDefault();
            log.Should().NotBeNull();
            log.LogLevel.Should().Be(LogLevel.Information);
        }
    }
}
