using Checkout.Diagnostics.Abstractions;
using Serilog;

namespace Checkout.FX.LoggingExample.Core.Tests
{
    public class HandlerTests
    {
        private IHandler _handler;

        private Mock<ILogger> _mockLogger;
        private Mock<IMetrics> _mockMetrics;

        public HandlerTests()
        {
            _mockLogger = new Mock<ILogger>();
            _mockMetrics = new Mock<IMetrics>();
            _handler = new Handler(_mockLogger.Object, _mockMetrics.Object);
        }

        [Fact]
        public void DoesExists()
        {
            _handler.Should().NotBeNull();
        }
    }
}
