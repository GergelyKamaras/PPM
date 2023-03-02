using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AuthService.Authentication;

public interface IJWTService
{
    JwtSecurityToken GenerateLoginJWT(ApplicationUser user);
    List<Claim> RetrieveUserClaims(ApplicationUser user);
    JwtSecurityToken CreateToken(List<Claim> claims);
}