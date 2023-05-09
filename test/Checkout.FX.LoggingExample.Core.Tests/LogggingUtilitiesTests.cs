using System.Linq;
using Serilog;
using Serilog.Events;
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

        [Fact]
        public void IncludeStringAttributeHasValue()
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
                .WithLevel(LogEventLevel.Information)
                .Appearing().Once()
                .WithProperty(key)
                .WithValue(value);
        }


        // TODO Generic test for types and lists
        [Fact]
        public void IncludeArrayAttributeHasValue()
        {
            // Arrange
            var value = new string[] { "USDGBP", "USDEUR" };

            // Act
            _logger
                .ForContext(key, value)
                .Information("Test");

            // Assert
            var property = InMemorySink.Instance.LogEvents.FirstOrDefault()
                .Properties.SingleOrDefault(property => property.Key == key);
            var type = property.Value.GetType();


            InMemorySink.Instance
                .Should()
                .HaveMessage("Test")
                .Appearing().Once()
                .WithProperty(key)
                .WhichValue<string[]>()
                .Should().Contain(value);
        }

        // TODO Generic test for types and lists
        [Fact]
        public void IncludeObjectAttributeHasValue()
        {
            // Arrange
            var value = new { Name = "bob the frog", Age = 30 };

            // Act
            _logger
                .ForContext(key, value)
                .Information("Test");

            // Assert
            var property = InMemorySink.Instance.LogEvents.FirstOrDefault()
                .Properties.SingleOrDefault(property => property.Key == key);

            // property.Value.Should().Be(value);

            InMemorySink.Instance
                .Should()
                .HaveMessage("Test")
                .Appearing().Once()
                .WithProperty(key)
                .WhichValue<string[]>();
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
