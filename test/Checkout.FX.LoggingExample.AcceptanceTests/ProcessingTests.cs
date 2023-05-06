namespace Checkout.FX.LoggingExample.AcceptanceTests
{
    [Story(Title = "Processing Lambda",
        AsA = "FX Analyst",
        IWant = "To process a message",
        SoThat = "a rate is retreieved")]
    public class ProcessingTests
    {
        [Fact]
        public void HandlerDoesProcess()
        {
            this.Given("Operating Normally")
                .When("Lambda is Invoked")
                .Then("Process Successfully")
                .BDDfy();
        }
    }
}
