using PlacesApp.UnitTest.Mock;
using Xunit;
using Xunit.Abstractions;

namespace PlacesApp.UnitTest
{
    public class ActionResponseTest
    {
        private readonly ITestOutputHelper _output = default;

        public ActionResponseTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ActionResponseTest_NullResult()
        {
            var testController = new TestController();
            var result = testController.AR_TestModel();

            _output.WriteLine($"Content: {result}");

            Assert.NotNull(result);
        }
    }
}
