using Microsoft.EntityFrameworkCore;
using PPMAPI.DataAccess.DbTableQueries.RentablePropertiesQueries;
using PPMAPI.DataAccess;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMAPITests
{
    internal class RentablePropertiesQueriesTests
    {
        private PPMDbContext _db;
        private IRentablePropertiesQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new RentablePropertiesQueries(_db);
        }

        [Test]
        public void AddRentableProperty_ValidInput_IsInDb()
        {
            RentableProperty RentableProperty = new RentableProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty"
            };

            _queries.AddRentableProperty(RentableProperty);

            Assert.That(_db.RentableProperties.Any(p => p.Id == RentableProperty.Id));
        }

        [Test]
        public void AddRentableProperty_InvalidInput_ThrowsError()
        {
            RentableProperty RentableProperty = new RentableProperty();
            RentableProperty.Address = null;
            Assert.Throws<DbUpdateException>(() => _queries.AddRentableProperty(RentableProperty));
        }

        [Test]
        public void AddRentableProperty_AddSameTwice_ThrowsError()
        {
            RentableProperty RentableProperty = new RentableProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty"
            };

            _queries.AddRentableProperty(RentableProperty);

            Assert.Throws<ArgumentException>(() => _queries.AddRentableProperty(RentableProperty));
        }

        [Test]
        public void DeleteRentableProperty_IsInDb_Success()
        {
            RentableProperty RentableProperty = new RentableProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty"
            };

            _db.RentableProperties.Add(RentableProperty);
            _db.SaveChanges();

            _queries.DeleteRentableProperty(RentableProperty.Id);

            Assert.That(_db.RentableProperties.FirstOrDefault(p => p.Id == RentableProperty.Id), Is.Null);
        }

        [Test]
        public void DeleteRentableProperty_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteRentableProperty(Guid.NewGuid()));
        }

        [Test]
        public void UpdateRentableProperty_IsInDb_Success()
        {
            RentableProperty RentableProperty = new RentableProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty"
            };

            _db.RentableProperties.Add(RentableProperty);
            _db.SaveChanges();

            string newName = "VeryRealRentableProperty";
            RentableProperty.Name = newName;

            _queries.UpdateRentableProperty(RentableProperty);

            Assert.That(_db.RentableProperties.FirstOrDefault(p => p.Id == RentableProperty.Id).Name, Is.SameAs(newName));

        }

        [Test]
        public void UpdateRentableProperty_NotInDb_ThrowsError()
        {
            RentableProperty RentableProperty = new RentableProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty"
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateRentableProperty(RentableProperty));
        }

        [Test]
        public void GetRentablePropertyById_IsInDb_MatchesOriginal()
        {
            RentableProperty RentableProperty = new RentableProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty"
            };

            _db.RentableProperties.Add(RentableProperty);
            _db.SaveChanges();

            Assert.That(_queries.GetRentablePropertyById(RentableProperty.Id), Is.SameAs(RentableProperty));
        }

        [Test]
        public void GetRentablePropertyById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetRentablePropertyById(Guid.NewGuid()), Is.Null);
        }

        [Test]
        public void GetRentablePropertiesByOwnerId_IsInDb_ReturnsResult()
        {
            RentableProperty RentableProperty = new RentableProperty()
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
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty"
            };

            _db.RentableProperties.Add(RentableProperty);
            _db.SaveChanges();

            Assert.That(_queries.GetRentablePropertiesByOwnerId(RentableProperty.Owner.UserId).Any(p => p.Id == RentableProperty.Id));
        }

        [Test]
        public void GetRentablePropertiesByOwnerId_MultipleInDb_ReturnsList()
        {
            Owner owner = new Owner()
            {
                UserId = "OwnerUserId"
            };


            RentableProperty RentableProperty1 = new RentableProperty()
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
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty"
            };

            RentableProperty RentableProperty2 = new RentableProperty()
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
                    Number = 974,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealRentableProperty2"
            };

            _db.RentableProperties.Add(RentableProperty1);
            _db.RentableProperties.Add(RentableProperty2);
            _db.SaveChanges();


            Assert.That(_queries.GetRentablePropertiesByOwnerId(owner.UserId).Any(p => p.Id == RentableProperty1.Id) &&
                        _queries.GetRentablePropertiesByOwnerId(owner.UserId).Any(p => p.Id == RentableProperty2.Id));
        }

        [Test]
        public void GetRentablePropertiesByOwnerId_NotInDb_ReturnsEmptyList()
        {
            string userId = "NotAUserId";
            Assert.That(_queries.GetRentablePropertiesByOwnerId(userId), Is.Empty);
        }
    }
}
