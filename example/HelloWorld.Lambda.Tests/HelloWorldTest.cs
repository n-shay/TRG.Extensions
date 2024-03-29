using Amazon.Lambda.TestUtilities;
using HelloWorld.Lambda.Models;
using TRG.Extensions.Diagnosis;
using Xunit;
using Xunit.Abstractions;

namespace HelloWorld.Lambda.Tests
{
    public class HelloWorldTest
    {
        private readonly ITestOutputHelper _output;

        public HelloWorldTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void InstantiatedAndExecutedOnce_Success()
        {
            Output response;
            var context = new TestLambdaContext();

            using (var bt = new BlockTelemeter("Test", _output.WriteLine, false))
            {
                var function = new Startup();
                bt.Snap("Startup");

                response = function.Handle(null, context);
            }

            Assert.NotNull(response);
            Assert.Equal("Hello World!", response.Message);
        }
    }
}
