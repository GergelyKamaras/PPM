using Microsoft.EntityFrameworkCore;
using PPMAPIDataAccess.DbTableQueries.RentalPropertiesQueries;
using PPMAPIDataAccess;
using PPMAPIModelLibrary.Models.UtilityModels;
using PPMAPIModelLibrary.Properties;
using PPMAPIModelLibrary.Users;

namespace PPMAPITests
{
    internal class RentalPropertiesQueriesTests
    {
        private PPMDbContext _db;
        private IRentalPropertiesQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new RentalPropertiesQueries(_db);
        }

        [Test]
        public void AddRentalProperty_ValidInput_IsInDb()
        {
            RentalProperty rentalProperty = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty"
            };

            _queries.AddRentalProperty(rentalProperty);

            Assert.That(_db.RentalProperties.Any(p => p.Id == rentalProperty.Id));
        }

        [Test]
        public void AddRentalProperty_InvalidInput_ThrowsError()
        {
            RentalProperty rentalProperty = new RentalProperty();
            rentalProperty.Address = null;
            Assert.Throws<DbUpdateException>(() => _queries.AddRentalProperty(rentalProperty));
        }

        [Test]
        public void AddRentalProperty_AddSameTwice_ThrowsError()
        {
            RentalProperty rentalProperty = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty"
            };

            _queries.AddRentalProperty(rentalProperty);

            Assert.Throws<ArgumentException>(() => _queries.AddRentalProperty(rentalProperty));
        }

        [Test]
        public void DeleteRentalProperty_IsInDb_Success()
        {
            RentalProperty rentalProperty = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty"
            };

            _db.RentalProperties.Add(rentalProperty);
            _db.SaveChanges();

            _queries.DeleteRentalProperty(rentalProperty.Id);

            Assert.That(_db.RentalProperties.FirstOrDefault(p => p.Id == rentalProperty.Id), Is.Null);
        }

        [Test]
        public void DeleteRentalProperty_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteRentalProperty(Guid.NewGuid()));
        }

        [Test]
        public void UpdateRentalProperty_IsInDb_Success()
        {
            RentalProperty rentalProperty = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty"
            };

            _db.RentalProperties.Add(rentalProperty);
            _db.SaveChanges();

            string newName = "VeryRealRentalProperty";
            rentalProperty.Name = newName;

            _queries.UpdateRentalProperty(rentalProperty);

            Assert.That(_db.RentalProperties.FirstOrDefault(p => p.Id == rentalProperty.Id).Name, Is.SameAs(newName));

        }

        [Test]
        public void UpdateRentalProperty_NotInDb_ThrowsError()
        {
            RentalProperty rentalProperty = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty"
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateRentalProperty(rentalProperty));
        }

        [Test]
        public void GetRentalPropertyById_IsInDb_MatchesOriginal()
        {
            RentalProperty rentalProperty = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty"
            };

            _db.RentalProperties.Add(rentalProperty);
            _db.SaveChanges();

            Assert.That(_queries.GetRentalPropertyById(rentalProperty.Id), Is.SameAs(rentalProperty));
        }

        [Test]
        public void GetRentalPropertyById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetRentalPropertyById(Guid.NewGuid()), Is.Null);
        }

        [Test]
        public void GetRentalPropertiesByOwnerId_IsInDb_ReturnsResult()
        {
            RentalProperty rentalProperty = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Owner = new Owner()
                {
                    UserId = "OwnerUserId"
                },
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty"
            };

            _db.RentalProperties.Add(rentalProperty);
            _db.SaveChanges();

            Assert.That(_queries.GetRentalPropertiesByOwnerId(rentalProperty.Owner.UserId).Any(p => p.Id == rentalProperty.Id));
        }

        [Test]
        public void GetRentalPropertiesByOwnerId_MultipleInDb_ReturnsList()
        {
            Owner owner = new Owner()
            {
                UserId = "OwnerUserId"
            };


            RentalProperty rentalProperty1 = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Owner = owner,
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty"
            };

            RentalProperty rentalProperty2 = new RentalProperty()
            {
                Id = Guid.NewGuid(),
                Owner = owner,
                Address = new Address()
                {
                    Id = 2,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 974,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentalProperty2"
            };

            _db.RentalProperties.Add(rentalProperty1);
            _db.RentalProperties.Add(rentalProperty2);
            _db.SaveChanges();


            Assert.That(_queries.GetRentalPropertiesByOwnerId(owner.UserId).Any(p => p.Id == rentalProperty1.Id) &&
                        _queries.GetRentalPropertiesByOwnerId(owner.UserId).Any(p => p.Id == rentalProperty2.Id));
        }

        [Test]
        public void GetRentalPropertiesByOwnerId_NotInDb_ReturnsEmptyList()
        {
            string userId = "NotAUserId";
            Assert.That(_queries.GetRentalPropertiesByOwnerId(userId), Is.Empty);
        }
    }
}
