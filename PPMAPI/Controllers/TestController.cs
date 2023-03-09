using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PPMAPI.Controllers
{
    [Route("")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IResult Test()
        {
            return Results.Ok("I'm working");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("/admintest")]
        public IResult AdminTest()
        {
            return Results.Ok("You're and admin Harry!");
        }

        [HttpGet]
        [Authorize]
        [Route("/authtest")]
        public IResult AuthTest()
        {
            return Results.Ok("You're authorized Harry!");
        }
    }
}
