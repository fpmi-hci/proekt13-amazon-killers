using HelloWorld;
using Xunit;

namespace HelloWorldTests
{
    public class SecondClassTests
    {
        [Fact]
        public void GetNameMessage()
        {
            SecondClass sc = new SecondClass();
            string s = sc.GetName();
            Assert.Equal("Second class!", s);
        }
    }
}