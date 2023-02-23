using AuthService.Authentication;
using AuthService.DataAccess;
using AuthService.DataAccess.UserTableQueries;
using Microsoft.EntityFrameworkCore;
using PPMModelLibrary.Enums;
using PPMModelLibrary.Models.Users;

namespace AuthServiceTests
{
    [TestFixture]
    public class AuthOperationsTests
    {
        private AuthDbContext _dbContext;
        private IUserTableQueries _queries;
        private IAuthOperations _operations;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _dbContext = new AuthDbContext(options);
            _queries = new UserTableQueries(_dbContext);

            _operations = new AuthOperations(_queries);
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
                    Role = UserRole.Administrator
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
                    Role = UserRole.Administrator
                };

                // Act 
                _operations.Register(user);

                // Assert
                Assert.Throws<System.ArgumentException>(() => _operations.Register(user));
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
                    Role = UserRole.Administrator
                };

                // Act
                _operations.Register(user);

                // Assert
                Assert.AreEqual(user, _dbContext.Users.FirstOrDefault(u => u.UserName == user.UserName));
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
                    Role = UserRole.Administrator
                };

                ApplicationUser user2 = new ApplicationUser()
                {
                    FirstName = "Jolan2",
                    LastName = "Hegyi2",
                    UserName = "Hegyine2",
                    Email = "hegyine@hegy.com",
                    PasswordHash = "pw2",
                    Salt = "1232",
                    Role = UserRole.Administrator
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
                    Role = UserRole.Administrator
                };

                ApplicationUser user2 = new ApplicationUser()
                {
                    FirstName = "Jolan2",
                    LastName = "Hegyi2",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com2",
                    PasswordHash = "pw2",
                    Salt = "1232",
                    Role = UserRole.Administrator
                };

                // Act
                _operations.Register(user1);

                // Assert
                Assert.Throws<ArgumentException>(() => _operations.Register(user2));
            }
        }
    }
}