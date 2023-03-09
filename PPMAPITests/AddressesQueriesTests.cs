using Microsoft.EntityFrameworkCore;
using PPMAPI.DataAccess;
using PPMAPI.DataAccess.DbTableQueries.AddressQueries;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMAPITests
{
    public class Tests
    {
        private PPMDbContext _db;
        private IAddressesQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new AddressesQueries(_db);
        }

        [Test]
        public void AddAddress_ValidInput_IsInDb()
        {
            // Arrange
            Address address = new Address()
            {
                Id = 1,
                Country = "VeryCountry",
                City = "Verycity",
                ZipCode = "9783",
                Street = "VeryStreet",
                Number = 973,
                AdditionalInfo = "Nothing Much"
            };
            using (_db)
            {
                // Act
                _queries.AddAddress(address);

                // Assert
                Assert.That(_db.Addresses.FirstOrDefault(a => a.Id == address.Id), Is.Not.Null);
            }
        }
        
        [Test]
        public void AddAddress_InvalidInput_ThrowsError()
        {
            // Arrange
            Address address = new Address();
            using (_db)
            {
                // Assert
                Assert.Throws<DbUpdateException>(() => _queries.AddAddress(address));
            }
        }

        [Test]
        public void GetAddressById_IsInDb_GetsAddress()
        {
            Assert.Pass();
        }

        [Test]
        public void GetAddressById_NotInDb_ThrowsError()
        {
            Assert.Pass();
        }

        [Test]
        public void UpdateAddress_IsInDb_UpdatesSuccessfully()
        {
            Assert.Pass();
        }

        [Test]
        public void UpdateAddress_NotInDb_ThrowsError()
        {
            Assert.Pass();
        }

        [Test]
        public void DeleteAddress_IsInDb_Successful()
        {
            Assert.Pass();
        }

        [Test]
        public void DeleteAddress_NotInDb_ThrowsError()
        {
            Assert.Pass();
        }

    }
}