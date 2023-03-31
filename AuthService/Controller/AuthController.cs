using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;
using AuthServiceServiceLayer.Authentication.AuthOperations;
using AuthServiceServiceLayer.Authentication.JWTService;
using AuthServiceServiceLayer.Authentication.Roles.Validator;
using AuthServiceServiceLayer.ModelConverter;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IConfiguration _config;

        public AuthController(IApplicationUserFactory factory, IAuthOperations ops, 
            IRoleValidator roleValidator, UserManager<ApplicationUser> userManager,
            IJWTService jwtService, IConfiguration config)
        {
            _factory = factory;
            _ops = ops;
            _roleValidator = roleValidator;
            _userManager = userManager;
            _jwtService = jwtService;
            _config = config;
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
            
            // Administrators don't need to be registered on the api
            var apiRegResult = user.Role == "Administrator" ? Results.Ok() : await RegisterUserOnAPI(user);

            if (userRegResult && roleRegResult == IdentityResult.Success &&
                apiRegResult == Results.Ok())
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

        [Authorize]
        public async Task<IResult> DeleteUser(string id)
        {

            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Results.Problem("Error, no such user found!");
            }

            if (await _userManager.DeleteAsync(user) == IdentityResult.Success)
            {
                return Results.Ok("User successfully deleted!");
            }

            return Results.Problem("Unexpected error during deletion!");
        }

        private async Task<IResult> RegisterUserOnAPI(ApplicationUser user)
        {
            Dictionary<string, string> userData = new Dictionary<string, string>()
            {
                { "userId", user.Id },
                {"role", user.Role}
            };
            var content = new FormUrlEncodedContent(userData);
            
            HttpClient client = new HttpClient();

            ApplicationUser adminOne = _ops.VerifyLoginDTO(new UserLoginDTO(){Email = _config["AdminOneEmail"], Password = _config["AdminOnePassword"] });
            
            JwtSecurityToken token = _jwtService.GenerateLoginJWT(adminOne);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(token));

            string domain = HttpContext.Request.Host.Host;
            string port = _config.GetSection("APIPort").Value;

            var response = await client.PostAsync($"https://{domain}:{port}/api/users", content);

            if (response.IsSuccessStatusCode)
            {
                return Results.Ok();
            }

            return Results.Problem("Something went wrong while registering the user on the API");
        }

        private async Task<IResult> DeleteUserOnAPI(string id)
        {
            HttpClient client = new HttpClient();

            ApplicationUser adminOne = _ops.VerifyLoginDTO(new UserLoginDTO() { Email = _config["AdminOneEmail"], Password = _config["AdminOnePassword"] });

            JwtSecurityToken token = _jwtService.GenerateLoginJWT(adminOne);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", new JwtSecurityTokenHandler().WriteToken(token));

            string domain = HttpContext.Request.Host.Host;
            string port = _config.GetSection("APIPort").Value;

            var response = await client.DeleteAsync($"https://{domain}:{port}/api/users/{id}");

            if (response.IsSuccessStatusCode)
            {
                return Results.Ok();
            }

            return Results.Problem("Something went wrong while registering the user on the API");
        }
    }
}
