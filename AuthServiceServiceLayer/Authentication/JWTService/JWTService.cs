using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthServiceServiceLayer.Authentication.JWTService
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _config;
        public JWTService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public JwtSecurityToken GenerateLoginJWT(ApplicationUser user)
        {
            List<Claim> claims = RetrieveUserClaims(user);
            return CreateToken(claims);
        }

        private List<Claim> RetrieveUserClaims(ApplicationUser user)
        {
            List<Claim> claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Role, user.Role));
            claims.Add(new Claim("Id", user.Id));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            return claims;
        }

        private JwtSecurityToken CreateToken(List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: claims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }
    }
}
