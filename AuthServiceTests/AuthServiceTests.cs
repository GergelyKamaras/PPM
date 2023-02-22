using AuthService;
using DataAccess;
using DataAccess.Enums;
using DataAccess.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace AuthServiceTests
{
    [TestFixture]
    public class AuthServiceTests
    {
        private AppDbContext _dbContext;
        private IAuthQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _dbContext = new AppDbContext(options);

            _queries = new AuthQueries(_dbContext);
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
                    Role = UserRole.Admin
                };
                
                // Assert
                Assert.IsTrue(_queries.Register(user));

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
                    Role = UserRole.Admin
                };
                
                // Act 
                _queries.Register(user);

                // Assert
                Assert.Throws<System.ArgumentException>(() => _queries.Register(user));
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
                Assert.Throws<DbUpdateException>(() => _queries.Register(user));
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
                    Role = UserRole.Admin
                };
                
                // Act
                _queries.Register(user);

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
                    Role = UserRole.Admin
                };

                ApplicationUser user2 = new ApplicationUser()
                {
                    FirstName = "Jolan2",
                    LastName = "Hegyi2",
                    UserName = "Hegyine2",
                    Email = "hegyine@hegy.com",
                    PasswordHash = "pw2",
                    Salt = "1232",
                    Role = UserRole.Admin
                };

                _queries.Register(user1);
                Assert.Throws<ArgumentException>(() => _queries.Register(user2));
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
                    Role = UserRole.Admin
                };

                ApplicationUser user2 = new ApplicationUser()
                {
                    FirstName = "Jolan2",
                    LastName = "Hegyi2",
                    UserName = "Hegyine",
                    Email = "hegyine@hegy.com2",
                    PasswordHash = "pw2",
                    Salt = "1232",
                    Role = UserRole.Admin
                };
                
                // Act
                _queries.Register(user1);

                // Assert
                Assert.Throws<ArgumentException>(() => _queries.Register(user2));
            }
        }
    }
}