using System.Security.Claims;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AuthService.Authentication;

public interface IJWTService
{
    JsonWebToken GenerateLoginJWT(ApplicationUser user);
    List<ClaimsIdentity> RetrieveUserClaims(ApplicationUser user);
    JsonWebToken CreateToken(List<ClaimsIdentity> claims);
}