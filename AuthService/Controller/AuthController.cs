using System.IdentityModel.Tokens.Jwt;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;
using AuthServiceServiceLayer.Authentication.AuthOperations;
using AuthServiceServiceLayer.Authentication.JWTService;
using AuthServiceServiceLayer.Authentication.Roles.Validator;
using AuthServiceServiceLayer.ModelConverter;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthServiceAPI.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationUserFactory _factory;
        private readonly IAuthOperations _ops;
        private readonly IRoleValidator _roleValidator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJWTService _jwtService;

        public AuthController(IApplicationUserFactory factory, IAuthOperations ops, 
            IRoleValidator roleValidator, UserManager<ApplicationUser> userManager,
            IJWTService jwtService)
        {
            _factory = factory;
            _ops = ops;
            _roleValidator = roleValidator;
            _userManager = userManager;
            _jwtService = jwtService;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IResult> Register([FromForm] UserRegistrationDTO userDTO)
        {
            bool validRole = await _roleValidator.Validate(userDTO.Role);
            if (!validRole)
            {
                return Results.Problem("Error, invalid user role!", statusCode: 500);
            }
            
            ApplicationUser user = _factory.Converter(userDTO);

            bool userRegResult = _ops.Register(user);

            IdentityResult roleRegResult = await _userManager.AddToRoleAsync(user, user.Role);

            if (userRegResult && roleRegResult == IdentityResult.Success)
            {
                return Results.Ok("Successfully registered user!");
            }
            
            return Results.Problem("Error registering user", statusCode:500);
        }

        [Route("login")]
        [HttpPost]
        public IResult Login([FromForm] UserLoginDTO userDTO)
        {
            ApplicationUser user;
            try
            {
                user = _ops.VerifyLoginDTO(userDTO);
            }
            catch (ArgumentException ex)
            {
                return Results.Problem("Wrong email and/or password" + ex, statusCode:500);
            }

            JwtSecurityToken token = _jwtService.GenerateLoginJWT(user);

            return Results.Ok(new
            {
                message = "Success",
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}
