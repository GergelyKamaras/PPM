using AuthService.Authentication;
using AuthService.ModelConverter;
using Microsoft.AspNetCore.Mvc;
using PPMModelLibrary.Models.InputDTOs;
using PPMModelLibrary.Models.Users;

namespace AuthService.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationUserFactory _factory;
        private readonly ISecurityUtil _secutil;
        private readonly IAuthOperations _ops;
        public AuthController(IApplicationUserFactory factory, ISecurityUtil secutil, IAuthOperations ops)
        {
            _factory = factory;
            _secutil = secutil;
            _ops = ops;
        }

        [Route("register")]
        [HttpPost]
        public IResult Register(UserRegistrationDTO userDTO)
        {
            ApplicationUser user = _factory.Converter(userDTO);
            if (_ops.Register(user))
            {
                return Results.Ok();
            }
            return Results.Problem("Error registering user", statusCode:500);
        }

        [HttpGet]
        public string Test()
        {
            return "Hello There";
        }
    }
}
