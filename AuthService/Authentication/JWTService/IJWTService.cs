using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AuthService.Authentication.JWTService.JWTService;

public interface IJWTService
{
    JwtSecurityToken GenerateLoginJWT(ApplicationUser user);
}