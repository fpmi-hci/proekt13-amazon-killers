using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        public HelloWorldController()
        {
        }

        [HttpGet(Name = "GetHelloWorld")]
        public String Get()
        {
            return "Hello, World!";
        }
    }
}