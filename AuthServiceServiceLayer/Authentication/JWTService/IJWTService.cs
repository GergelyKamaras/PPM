using System.IdentityModel.Tokens.Jwt;
using AuthServiceModelLibrary.ApplicationUser;

namespace AuthServiceServiceLayer.Authentication.JWTService;

public interface IJWTService
{
    JwtSecurityToken GenerateLoginJWT(ApplicationUser user);
}