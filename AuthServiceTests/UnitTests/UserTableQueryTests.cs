using AuthService.DataAccess;
using AuthService.DataAccess.UserTableQueries;
using Microsoft.EntityFrameworkCore;
using AuthServiceModelLibrary.Enums;
using AuthServiceModelLibrary.ApplicationUser;

namespace AuthServiceTests.UnitTests
{
    public class UserTableQueryTests
    {
        private AuthDbContext _dbContext;
        private IUserTableQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<AuthDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _dbContext = new AuthDbContext(options);

            _queries = new UserTableQueries(_dbContext);
        }

        [Test]
        public void AddUser_ValidInput_IsInDb()
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
                _queries.AddUser(user);

                // Assert
                Assert.That(_dbContext.Users.FirstOrDefault(u => u.Email == user.Email), Is.SameAs(user));
            }
        }

        [Test]
        public void AddUser_AddEmptyModel_ThrowsError()
        {
            // Arrange
            using (_dbContext)
            {
                ApplicationUser user = new ApplicationUser();

                // Assert
                Assert.Throws<DbUpdateException>(() => _queries.AddUser(user));
            }
        }

        [Test]
        public void AddUser_AddSameUserTwice_ThrowsError()
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
                _queries.AddUser(user);

                // Assert
                Assert.Throws<ArgumentException>(() => _queries.AddUser(user));
            }
        }

        [Test]
        public void UpdateUser_ValidInput_IsUpdated()
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
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                string newEmail = "nemjolan@hegy.com";
                user.Email = newEmail;

                _queries.UpdateUser(user);

                // Assert
                Assert.That(_dbContext.Users.FirstOrDefault(u => u.Email == user.Email), Is.SameAs(user));
            }
        }

        [Test]
        public void UpdateUser_MissingUser_ThrowsError()
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
                Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateUser(user));
            }
        }

        [Test]
        public void DeleteUser_IsInDb_SuccessfulDelete()
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
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                _queries.DeleteUser(user.Id);

                // Assert
                Assert.IsNull(_dbContext.Users.FirstOrDefault(u => u.Email == user.Email));
            }
        }

        [Test]
        public void DeleteUser_NotInDb_ThrowsError()
        {
            // Arrange
            using (_dbContext)
            {
                // Assert
                Assert.Throws<InvalidOperationException>(() => _queries.DeleteUser("NemId"));
            }
        }

        [Test]
        public void GetUserById_InDb_Successful()
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
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                // Assert
                Assert.That(_queries.GetUserById(user.Id), Is.SameAs(user));
            }
        }

        [Test]
        public void GetUserByEmail_InDb_Successful()
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
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                // Assert
                Assert.That(_queries.GetUserByEmail(user.Email), Is.SameAs(user));
            }
        }

        [Test]
        public void GetUserByUsername_InDb_Successful()
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
                _dbContext.Users.Add(user);
                _dbContext.SaveChanges();

                // Assert
                Assert.That(_queries.GetUserByUsername(user.UserName), Is.SameAs(user));
            }
        }

        [Test]
        public void GetUserById_NotInDb_ReturnsNull()
        {
            // Assert
            Assert.That(_queries.GetUserById("NotAnId"), Is.Null);
        }

        [Test]
        public void GetUserByEmail_NotInDb_ReturnsNull()
        {
            // Assert
            Assert.That(_queries.GetUserByEmail("NotAnEmail"), Is.Null);
        }

        [Test]
        public void GetUserByUsername_NotInDb_ReturnsNull()
        {
            // Assert
            Assert.That(_queries.GetUserByUsername("NotAUsername"), Is.Null);
        }
    }
}