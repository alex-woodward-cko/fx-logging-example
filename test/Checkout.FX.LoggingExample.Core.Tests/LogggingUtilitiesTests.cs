using System.Collections.Generic;
using System.Linq;
using MELT;
using Microsoft.Extensions.Logging;

namespace Checkout.FX.LoggingExample.Core.UnitTests
{
    public class LogggingUtilitiesTests
    {
        const string key = "test-property";

        ITestLoggerFactory _loggerFactory;
        ILogger _logger;

        public LogggingUtilitiesTests()
        {
            _loggerFactory = TestLoggerFactory.Create();
            _logger = _loggerFactory.CreateLogger<LogggingUtilitiesTests>();
        }

        // TODO Generic test for types and lists
        [Fact]
        public void IncludeAttributeHasValue()
        {
            // Arrange
            var value = "USDGBP";

            // Act
            using (_logger.BeginScope(LoggingUtlities.AddAttribute((key, value))))
            {
                _logger.LogInformation("Test");
            }

            // Assert
            var log = _loggerFactory.GetLogs().FirstOrDefault();
            log.TryGetPropertyValue<string>(key, out var property).Should().BeTrue();
            property.FirstOrDefault().Should().Be(value);
        }

        [Fact]
        public void NullOrEmptyAttributeHasValue()
        {
            // Arrange

            // Act
            using (_logger.BeginScope(LoggingUtlities.AddAttribute((key, null))))
            {
                _logger.LogInformation("Test");
            }

            // Assert
            var log = _loggerFactory.GetLogs().FirstOrDefault();
            log.TryGetPropertyValue<string>(key, out var property).Should().BeTrue();
            property.FirstOrDefault().Should().Be("(null)");
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void EmptyAttributeHasValue(string empty)
        {
            // Arrange

            // Act
            using (_logger.BeginScope(LoggingUtlities.AddAttribute((key, empty))))
            {
                _logger.LogInformation("Test");
            }

            // Assert
            var log = _loggerFactory.GetLogs().FirstOrDefault();
            log.TryGetPropertyValue<string>(key, out var property).Should().BeTrue();
            property.FirstOrDefault().Should().BeNullOrWhiteSpace();
        }
    }
}
