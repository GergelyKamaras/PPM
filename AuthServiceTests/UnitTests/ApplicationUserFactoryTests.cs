using AuthService.Authentication.SecurityUtil;
using AuthService.ModelConverter;
using AuthServiceModelLibrary.DTOs;
using AuthServiceModelLibrary.ApplicationUser;

namespace AuthServiceTests.UnitTests
{
    internal class ApplicationUserFactoryTests
    {
        private IApplicationUserFactory _factory;

        [SetUp]
        public void Setup()
        {
            ISecurityUtil util = new SecurityUtil();
            _factory = new ApplicationUserFactory(util);
        }

        [Test]
        public void Converter_ValidInput_HasId()
        {
            // Arrange
            UserRegistrationDTO user = new UserRegistrationDTO()
            {
                Email = "hegyiember@hegy.com",
                FirstName = "Mr.",
                LastName = "Hegyi",
                Password = "SzeretemAHegyeket",
                Role = "Administrator",
                Username = "Hegyiember"
            };

            // Act
            ApplicationUser appUser = _factory.Converter(user);

            // Assert
            Assert.That(appUser.Id, Is.Not.Empty);
        }

        [Test]
        public void Converter_ValidInput_HasSalt()
        {
            // Arrange
            UserRegistrationDTO user = new UserRegistrationDTO()
            {
                Email = "hegyiember@hegy.com",
                FirstName = "Mr.",
                LastName = "Hegyi",
                Password = "SzeretemAHegyeket",
                Role = "Administrator",
                Username = "Hegyiember"
            };

            // Act
            ApplicationUser appUser = _factory.Converter(user);

            // Assert
            Assert.That(appUser.Salt, Is.Not.Empty);
        }

        [Test]
        public void Converter_ValidInput_HasNewPassword()
        {
            // Arrange
            UserRegistrationDTO user = new UserRegistrationDTO()
            {
                Email = "hegyiember@hegy.com",
                FirstName = "Mr.",
                LastName = "Hegyi",
                Password = "SzeretemAHegyeket",
                Role = "Administrator",
                Username = "Hegyiember"
            };

            // Act
            ApplicationUser appUser = _factory.Converter(user);

            // Assert
            Assert.That(appUser.PasswordHash, Is.Not.EqualTo(user.Password));
        }
    }
}
