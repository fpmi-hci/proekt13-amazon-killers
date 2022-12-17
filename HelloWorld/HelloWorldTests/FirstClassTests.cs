using HelloWorld;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldTests
{
    public class FirstClassTests
    {
        [Fact]
        public void GetNameMessage()
        {
            FirstClass fc = new FirstClass();
            string s = fc.GetName();
            Assert.Equal("First class!", s);
        }
    }
}