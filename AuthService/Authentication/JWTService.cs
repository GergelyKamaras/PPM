using System.Security.Claims;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.IdentityModel.JsonWebTokens;

namespace AuthService.Authentication
{
    public class JWTService : IJWTService
    {
        public JsonWebToken GenerateLoginJWT(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public List<ClaimsIdentity> RetrieveUserClaims(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public JsonWebToken CreateToken(List<ClaimsIdentity> claims)
        {
            throw new NotImplementedException();
        }
    }
}
