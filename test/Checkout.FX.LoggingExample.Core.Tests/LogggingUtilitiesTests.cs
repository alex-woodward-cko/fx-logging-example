using Serilog;
using Serilog.Sinks.InMemory;
using Serilog.Sinks.InMemory.Assertions;

namespace Checkout.FX.LoggingExample.Core.UnitTests
{
    public class LogggingUtilitiesTests
    {
        const string key = "test-property";

        ILogger _logger;

        public LogggingUtilitiesTests()
        {
            _logger = LogTestingExtensions.Logger;
        }

        // TODO Generic test for types and lists
        [Fact]
        public void IncludeAttributeHasValue()
        {
            // Arrange
            var value = "USDGBP";

            // Act
            _logger
                .ForContext(key, value)
                .Information("Test");

            // Assert
            InMemorySink.Instance
                .Should()
                .HaveMessage("Test")
                .Appearing().Once();
        }

        [Fact]
        public void NullOrEmptyAttributeHasValue()
        {
            // Arrange

            // Act
            _logger
                .ForContext(key, null)
                .Information("Test");

            // Assert
            InMemorySink.Instance
                .Should()
                .HaveMessage("Test")
                .Appearing().Once();
        }

        [Theory]
        [InlineData("")]
        [InlineData("  ")]
        public void EmptyAttributeHasValue(string empty)
        {
            // Arrange

            // Act
            _logger
                .ForContext(key, empty)
                .Information("Test");

            // Assert
            InMemorySink.Instance
                .Should()
                .HaveMessage("Test")
                .Appearing().Once();
        }
    }
}
