using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService;

namespace AuthServiceTests
{
    [TestFixture]
    internal class SecurityUtilTests
    {
        ISecurityUtil _securityUtil;
        [SetUp]
        public void Setup()
        {
            _securityUtil = new SecurityUtil();
        }

        [Test]
        public void VerifyPassword_MatchingPasswords_ReturnsTrue()
        {
            // Arrange 
            string password = "Jolan";
            string salt = _securityUtil.CreateSalt();
            string hashedPassword = _securityUtil.HashPassword(password, salt);

            // Assert
            Assert.IsTrue(_securityUtil.VerifyPassword(password, salt, hashedPassword));
        }
        
    }
}
