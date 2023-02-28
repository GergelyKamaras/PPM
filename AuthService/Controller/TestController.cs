using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controller
{
    [Route("")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string TestGet()
        {
            return "Hello there!";
        }
    }
}
