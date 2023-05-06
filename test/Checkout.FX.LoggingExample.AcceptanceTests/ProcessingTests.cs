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
            this.Given(s => s.NormalOperation())
                .When(s => s.LambdaIsInvoked())
                .Then(s => s.SuccessfullyProcess())
                .BDDfy();
        }

        private void NormalOperation()
        {
        }

        private void LambdaIsInvoked()
        {
        }

        private void SuccessfullyProcess()
        {
        }
    }
}
