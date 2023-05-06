using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Checkout.Diagnostics.Abstractions;
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
            // Act
            await _handler.HandleAsync(CancellationToken.None);

            // Assert
            VerifyInfoLog_Starting();
            VerifyInfoLog_Processing();
            VerifyInfoLog_Completed();
        }

        private void VerifyInfoLog_Starting()
        {
            var log = _loggerFactory.Sink.LogEntries.FirstOrDefault(log  => log.Message == "Starting...");
            log.Should().NotBeNull();
            log.LogLevel.Should().Be(LogLevel.Information);
        }

        private void VerifyInfoLog_Processing()
        {
            var log = _loggerFactory.Sink.LogEntries.FirstOrDefault(log => log.Message == "Processing...");
            log.Should().NotBeNull();
            log.LogLevel.Should().Be(LogLevel.Information);
        }

        private void VerifyInfoLog_Completed()
        {
            var log = _loggerFactory.Sink.LogEntries.FirstOrDefault(log => log.Message == "Completed");
            log.Should().NotBeNull();
            log.LogLevel.Should().Be(LogLevel.Information);
        }
    }
}
