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
        public IResult TestGet()
        {
            return Results.Ok("Hello there!");
        }

        [HttpGet]
        [Route("admintest")]
        [Authorize(Roles = nameof(UserRoles.Administrator))]
        public IResult AdminTest()
        {
            return Results.Ok("You're an admin Harry!");
        }

        [HttpGet]
        [Route("authtest")]
        [Authorize]
        public IResult AuthTest()
        {
            return Results.Ok("You're authorized Harry!");
        }
    }
}
