using System.IdentityModel.Tokens.Jwt;
using AuthServiceModelLibrary.ApplicationUser;

namespace AuthService.Authentication.JWTService;

public interface IJWTService
{
    JwtSecurityToken GenerateLoginJWT(ApplicationUser user);
}