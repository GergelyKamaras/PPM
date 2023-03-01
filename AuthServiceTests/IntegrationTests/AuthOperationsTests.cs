using AuthService.Authentication;
using AuthService.DataAccess;
using AuthService.DataAccess.UserTableQueries;
using Microsoft.EntityFrameworkCore;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;

namespace AuthServiceTests.IntegrationTests
{
    [TestFixture]
    public class AuthOperationsTests
    {
        private AuthDbContext _dbContext;
        private IUserTableQueries _queries;
        private IAuthOperations _operations;
        private ISecurityUtil _securityUtil;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _dbContext = new AuthDbContext(options);
            _queries = new UserTableQueries(_dbContext);
            _securityUtil = new SecurityUtil();

            _operations = new AuthOperations(_queries, _securityUtil);
        }

        [Test]
        public void Register_ValidInput_ReturnsTrue()
        {
            // Arrange
            using (_dbContext)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "Jolan",
                    LastName = "Hegyi",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com",
                    PasswordHash = "pw",
                    Salt = "123",
                    Role = "Administrator"
                };

                // Assert
                Assert.IsTrue(_operations.Register(user));
            }
        }

        [Test]
        public void Register_AddSameUserTwice_ThrowsError()
        {
            // Arrange
            using (_dbContext)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "Jolan",
                    LastName = "Hegyi",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com",
                    PasswordHash = "pw",
                    Salt = "123",
                    Role = "Administrator"
                };

                // Act 
                _operations.Register(user);

                // Assert
                Assert.Throws<ArgumentException>(() => _operations.Register(user));
            }
        }

        [Test]
        public void Register_AddIncompleteUser_ThrowsError()
        {
            // Arrange
            using (_dbContext)
            {
                ApplicationUser user = new ApplicationUser();

                // Assert
                Assert.Throws<DbUpdateException>(() => _operations.Register(user));
            }
        }

        [Test]
        public void Register_ValidInput_IsInDb()
        {
            // Arrange
            using (_dbContext)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "Jolan",
                    LastName = "Hegyi",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com",
                    PasswordHash = "pw",
                    Salt = "123",
                    Role = "Administrator"
                };

                // Act
                _operations.Register(user);

                // Assert
                Assert.That(_dbContext.Users.FirstOrDefault(u => u.UserName == user.UserName), Is.EqualTo(user));
            }
        }

        [Test]
        public void Register_AddSameEmailTwice_ThrowsError()
        {
            // Arrange
            using (_dbContext)
            {
                ApplicationUser user1 = new ApplicationUser()
                {
                    FirstName = "Jolan",
                    LastName = "Hegyi",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com",
                    PasswordHash = "pw",
                    Salt = "123",
                    Role = "Administrator"
                };

                ApplicationUser user2 = new ApplicationUser()
                {
                    FirstName = "Jolan2",
                    LastName = "Hegyi2",
                    UserName = "Hegyine2",
                    Email = "hegyine@hegy.com",
                    PasswordHash = "pw2",
                    Salt = "1232",
                    Role = "Administrator"
                };

                _operations.Register(user1);
                Assert.Throws<ArgumentException>(() => _operations.Register(user2));
            }
        }

        [Test]
        public void Register_AddSameUsernameTwice_ThrowsError()
        {
            // Arrange
            using (_dbContext)
            {
                ApplicationUser user1 = new ApplicationUser()
                {
                    FirstName = "Jolan",
                    LastName = "Hegyi",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com",
                    PasswordHash = "pw",
                    Salt = "123",
                    Role = "Administrator"
                };

                ApplicationUser user2 = new ApplicationUser()
                {
                    FirstName = "Jolan2",
                    LastName = "Hegyi2",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com2",
                    PasswordHash = "pw2",
                    Salt = "1232",
                    Role = "Administrator"
                };

                // Act
                _operations.Register(user1);

                // Assert
                Assert.Throws<ArgumentException>(() => _operations.Register(user2));
            }
        }

        [Test]
        public void Login_ExistingUser_ReturnSameUser()
        {
            // Arrange
            using (_dbContext)
            {
                string password = "pw";
                string salt = _securityUtil.CreateSalt();

                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "Jolan",
                    LastName = "Hegyi",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com",
                    Salt = salt,
                    PasswordHash = _securityUtil.HashPassword(password, salt),
                    Role = "Administrator"
                };

                // Act
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                IUserLoginDTO userDTO = new UserLoginDTO();
                userDTO.Email = user.Email;
                userDTO.Password = password;

                // Assert
                Assert.That(user, Is.SameAs(_operations.Login(userDTO)));
            }
        }

        [Test]
        public void Login_MissingUser_ThrowsError()
        {
            // Arrange
            IUserLoginDTO userLoginDto = new UserLoginDTO();
            userLoginDto.Email = "NotAnEmail";
            userLoginDto.Password = "NotAPassword";

            // Assert
            Assert.Throws<ArgumentException>(() => _operations.Login(userLoginDto));
        }

        [Test]
        public void Login_WrongPassword_ThrowsError()
        {
            // Arrange
            using (_dbContext)
            {
                string password = "pw";
                string salt = _securityUtil.CreateSalt();

                ApplicationUser user = new ApplicationUser()
                {
                    FirstName = "Jolan",
                    LastName = "Hegyi",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com",
                    Salt = salt,
                    PasswordHash = _securityUtil.HashPassword(password, salt),
                    Role = "Administrator"
                };

                // Act
                _dbContext.Users.Add(user);
                IUserLoginDTO userDTO = new UserLoginDTO();
                userDTO.Email = user.Email;
                userDTO.Password = "WrongPassword";

                // Assert
                Assert.Throws<ArgumentException>(() => _operations.Login(userDTO));
            }
        }
    }
}