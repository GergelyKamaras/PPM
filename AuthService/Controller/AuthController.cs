using AuthService.Authentication;
using AuthService.ModelConverter;
using Microsoft.AspNetCore.Mvc;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.InputDTOs;

namespace AuthService.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationUserFactory _converter;
        private readonly ISecurityUtil _secutil;
        public AuthController(IApplicationUserFactory converter, ISecurityUtil secutil)
        {
            _converter = converter;
            _secutil = secutil;
        }

        [Route("register")]
        [HttpPost]
        public IResult Register(UserRegistrationDTO user)
        {
            return Results.Ok();
        }
    }
}
