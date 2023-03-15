using Microsoft.EntityFrameworkCore;
using PPMAPIDataAccess;
using PPMAPIDataAccess.DbTableQueries.PropertiesQueries;
using PPMAPIModelLibrary.UtilityModels;
using PPMAPIModelLibrary.Properties;
using PPMAPIModelLibrary.Users;

namespace PPMAPITests
{
    internal class PropertiesQueriesTests
    {
        private PPMDbContext _db;
        private IPropertiesQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new PropertiesQueries(_db);
        }

        [Test]
        public void AddProperty_ValidInput_IsInDb()
        {
            Property property = new Property()
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
                Name = "RealProperty"
            };

            _queries.AddProperty(property);

            Assert.That(_db.Properties.Any(p => p.Id == property.Id));
        }

        [Test]
        public void AddProperty_InvalidInput_ThrowsError()
        {
            Property property = new Property();
            property.Address = null;
            Assert.Throws<DbUpdateException>(() => _queries.AddProperty(property));
        }

        [Test]
        public void AddProperty_AddSameTwice_ThrowsError()
        {
            Property property = new Property()
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
                Name = "RealProperty"
            };

            _queries.AddProperty(property);

            Assert.Throws<ArgumentException>(() => _queries.AddProperty(property));
        }

        [Test]
        public void DeleteProperty_IsInDb_Success()
        {
            Property property = new Property()
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
                Name = "RealProperty"
            };

            _db.Properties.Add(property);
            _db.SaveChanges();

            _queries.DeleteProperty(property.Id);

            Assert.That(_db.Properties.FirstOrDefault(p => p.Id == property.Id), Is.Null);
        }

        [Test]
        public void DeleteProperty_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteProperty(Guid.NewGuid()));
        }

        [Test]
        public void UpdateProperty_IsInDb_Success()
        {
            Property property = new Property()
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
                Name = "RealProperty"
            };

            _db.Properties.Add(property);
            _db.SaveChanges();

            string newName = "VeryRealProperty";
            property.Name = newName;

            _queries.UpdateProperty(property);

            Assert.That(_db.Properties.FirstOrDefault(p => p.Id == property.Id).Name, Is.SameAs(newName));

        }

        [Test]
        public void UpdateProperty_NotInDb_ThrowsError()
        {
            Property property = new Property()
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
                Name = "RealProperty"
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateProperty(property));
        }

        [Test]
        public void GetPropertyById_IsInDb_MatchesOriginal()
        {
            Property property = new Property()
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
                Name = "RealProperty"
            };

            _db.Properties.Add(property);
            _db.SaveChanges();

            Assert.That(_queries.GetPropertyById(property.Id), Is.SameAs(property));
        }

        [Test]
        public void GetPropertyById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetPropertyById(Guid.NewGuid()), Is.Null);
        }

        [Test]
        public void GetPropertiesByOwnerId_IsInDb_ReturnsResult()
        {
            Property property = new Property()
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
                Name = "RealProperty"
            };

            _db.Properties.Add(property);
            _db.SaveChanges();

            Assert.That(_queries.GetPropertiesByOwnerId(property.Owner.UserId).Any(p => p.Id == property.Id));
        }

        [Test]
        public void GetPropertiesByOwnerId_MultipleInDb_ReturnsList()
        {
            Owner owner = new Owner()
            {
                UserId = "OwnerUserId"
            };
            

            Property property1 = new Property()
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
                Name = "RealProperty"
            };

            Property property2 = new Property()
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
                Name = "RealProperty2"
            };

            _db.Properties.Add(property1);
            _db.Properties.Add(property2);
            _db.SaveChanges();


            Assert.That(_queries.GetPropertiesByOwnerId(owner.UserId).Any(p => p.Id == property1.Id) &&
                        _queries.GetPropertiesByOwnerId(owner.UserId).Any(p => p.Id == property2.Id));
        }

        [Test]
        public void GetPropertiesByOwnerId_NotInDb_ReturnsEmptyList()
        {
            string userId = "NotAUserId";
            Assert.That(_queries.GetPropertiesByOwnerId(userId), Is.Empty);
        }
    }
}
