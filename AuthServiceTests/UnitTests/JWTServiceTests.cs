using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AuthService.Authentication.JWTService;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.Extensions.Configuration;

namespace AuthServiceTests.UnitTests
{
    internal class JWTServiceTests
    {
        private IJWTService _service;
        private ApplicationUser _user;
        
        [SetUp]
        public void SetUp()
        {
            IConfiguration config = new ConfigurationManager();
            config["JWT:ValidIssuer"] = "TestIssuer";
            config["JWT:ValidAudience"] = "TestAudience";
            config["JWT:Secret"] = "SuperSecret";
            _service = new JWTService(config);

            _user = new ApplicationUser()
            {
                Id = "123",
                FirstName = "Jolan",
                LastName = "Hegyi",
                UserName = "Hegyine",
                Email = "hegyine@hegy.com",
                PasswordHash = "pw",
                Salt = "123",
                Role = "Administrator"
            };
        }

        [Test]
        public void GenerateLoginJWT_ValidUser_IdsMatch()
        {
            // Act
            JwtSecurityToken token = _service.GenerateLoginJWT(_user); 

            // Assert
            Assert.That(new Claim("Id", _user.Id).Value, 
                Is.EqualTo(token.Claims.First(c => c.Type == "Id").Value));
        }

        [Test]
        public void GenerateLoginJWT_ValidUser_RolesMatch()
        {
            // Act
            JwtSecurityToken token = _service.GenerateLoginJWT(_user);

            // Assert
            Assert.That(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value,
                Is.SameAs(new Claim(ClaimTypes.Role, _user.Role).Value));
        }

        [Test]
        public void GenerateLoginJWT_ValidUser_EmailsMatch()
        {
            // Act
            JwtSecurityToken token = _service.GenerateLoginJWT(_user);

            // Assert
            Assert.That(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value,
                Is.EqualTo(new Claim(ClaimTypes.Email, _user.Email).Value));
        }

        [Test]
        public void GenerateLoginJWT_ValidUser_UsernamesMatch()
        {
            // Act
            JwtSecurityToken token = _service.GenerateLoginJWT(_user);
            
            // Assert
            Assert.That(token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value, 
                Is.EqualTo(new Claim(ClaimTypes.Name, _user.UserName).Value));
        }

        [Test]
        public void GenerateLoginJWT_InvalidUser_ThrowsError()
        {
            // Arrange
            ApplicationUser user = new ApplicationUser();

            //Assert
            Assert.Throws<ArgumentNullException>(() => _service.GenerateLoginJWT(user));

        }
    }
}
