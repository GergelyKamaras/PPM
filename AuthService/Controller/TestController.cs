using AuthService.Authentication.Roles;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Route("admintest")]
        [Authorize(Roles = nameof(UserRoles.Administrator))]
        public string AdminTest()
        {
            return "You're an admin Harry!";
        }

        [HttpGet]
        [Route("authtest")]
        [Authorize]
        public string AuthTest()
        {
            return "You're authorized Harry!";
        }
    }
}
