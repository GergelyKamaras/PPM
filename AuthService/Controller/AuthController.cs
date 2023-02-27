using AuthService.Authentication;
using AuthService.ModelConverter;
using Microsoft.AspNetCore.Mvc;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;

namespace AuthService.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationUserFactory _factory;
        private readonly IAuthOperations _ops;
        public AuthController(IApplicationUserFactory factory, IAuthOperations ops)
        {
            _factory = factory;
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

        [Route("login")]
        [HttpPost]
        public IResult Login(UserLoginDTO userDTO)
        {
            try
            {
                return Results.Ok(_ops.Login(userDTO));
            }
            catch (ArgumentException ex)
            {
                return Results.Problem("Wrong email and/or password", statusCode:500);
            }
        }
    }
}
