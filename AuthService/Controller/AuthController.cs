using AuthService.Authentication;
using AuthService.Authentication.Roles.Validator;
using AuthService.ModelConverter;
using Microsoft.AspNetCore.Mvc;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Controller
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IApplicationUserFactory _factory;
        private readonly IAuthOperations _ops;
        private readonly IRoleValidator _roleValidator;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(IApplicationUserFactory factory, IAuthOperations ops, 
            IRoleValidator roleValidator, UserManager<ApplicationUser> userManager)
        {
            _factory = factory;
            _ops = ops;
            _roleValidator = roleValidator;
            _userManager = userManager;
        }

        [Route("register")]
        [HttpPost]
        public async Task<IResult> Register(UserRegistrationDTO userDTO)
        {
            bool validRole = await _roleValidator.validate(userDTO.Role);
            if (!validRole)
            {
                return Results.Problem("Error, invalid user role!", statusCode: 500);
            }
            
            ApplicationUser user = _factory.Converter(userDTO);

            bool userRegResult = _ops.Register(user);

            IdentityResult roleRegResult = await _userManager.AddToRoleAsync(user, user.Role);

            if (userRegResult && roleRegResult == IdentityResult.Success)
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
