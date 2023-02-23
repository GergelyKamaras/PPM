using AuthService.Authentication;

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
            string password = "pw";
            string salt = _securityUtil.CreateSalt();
            string hashedPassword = _securityUtil.HashPassword(password, salt);

            // Assert
            Assert.IsTrue(_securityUtil.VerifyPassword(password, salt, hashedPassword));
        }

        [Test]
        public void HashPassword_ValidInput_TransformsString()
        {
            // Arrange
            string password = "pw";
            string salt = _securityUtil.CreateSalt();
            string hashedPassword = _securityUtil.HashPassword(password, salt);

            Assert.That(password, Is.Not.EqualTo(hashedPassword));
        }

        [Test]
        public void HashPassword_DifferentSalts_outputDiffers()
        {
            // Arrange
            string password = "pw";
            string salt1 = _securityUtil.CreateSalt();
            string hashedPassword1 = _securityUtil.HashPassword(password, salt1);

            string salt2 = _securityUtil.CreateSalt();
            string hashedPassword2 = _securityUtil.HashPassword(password, salt2);

            Assert.That(hashedPassword1, Is.Not.EqualTo(hashedPassword2));
        }
        
    }
}
