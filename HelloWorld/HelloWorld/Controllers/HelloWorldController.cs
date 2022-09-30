using Microsoft.AspNetCore.Mvc;

namespace HelloWorld.Controllers
{
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        public HelloWorldController()
        {
        }

        public String Get()
        {
            return "Hello, World!";
        }
    }
}
