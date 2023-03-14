using Microsoft.EntityFrameworkCore;
using PPMAPI.DataAccess.DbTableQueries.OwnersQueries;
using PPMAPI.DataAccess;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMAPITests
{
    internal class OwnersQueriesTests
    {
        private PPMDbContext _db;
        private IOwnersQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new OwnersQueries(_db);
        }

        [Test]
        public void AddOwner_ValidInput_IsInDb()
        {
            Owner owner = new Owner()
            {
                UserId = "UserId"
            };

            _queries.AddOwner(owner);

            Assert.That(_db.Owners.FirstOrDefault(o => o.UserId == owner.UserId), Is.SameAs(owner));
        }

        [Test]
        public void AddOwner_InvalidInput_ThrowsError()
        {
            Assert.Throws<InvalidOperationException>(() => _queries.AddOwner(new Owner()));
        }

        [Test]
        public void GetOwnerById_IsInDb_GetsOwner()
        {
            Owner owner = new Owner()
            {
                UserId = "UserId"
            };

            _queries.AddOwner(owner);

            Assert.That(_queries.GetOwnerById(owner.UserId), Is.SameAs(owner));
        }

        [Test]
        public void GetOwnerById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetOwnerById("NotAnId"), Is.Null);
        }

        [Test]
        public void GetOwnerByPropertyId_UsePropertyIsInDb_GetsOwner()
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
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealProperty"
            };

            Owner owner = new Owner()
            {
                UserId = "UserId"
            };

            owner.Properties.Add(property);


            _db.Owners.Add(owner);
            _db.Properties.Add(property);
            _db.SaveChanges();

            Assert.That(_queries.GetOwnerByPropertyId(property.Id), Is.SameAs(owner));
        }

        [Test]
        public void GetOwnerByPropertyId_UseRentalPropertyIsInDb_GetsOwner()
        {
            RentalProperty property = new RentalProperty()
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
                Name = "RealProperty"
            };

            Owner owner = new Owner()
            {
                UserId = "UserId"
            };

            owner.RentalProperties.Add(property);


            _db.Owners.Add(owner);
            _db.SaveChanges();

            Assert.That(_queries.GetOwnerByPropertyId(property.Id), Is.SameAs(owner));
        }

        [Test]
        public void GetOwnerByPropertyId_InvalidId_ReturnsEmptyList()
        {
            Assert.That(_queries.GetOwnerByPropertyId(Guid.NewGuid()), Is.Null);
        }

        [Test]
        public void UpdateOwner_IsInDb_UpdatesSuccessfully()
        {
            Owner owner = new Owner()
            {
                UserId = "UserId"
            };

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
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealProperty"
            };

            owner.Properties.Add(property);

            _db.Owners.Add(owner);
            _db.SaveChanges();

            owner.Properties.Clear();

            _queries.UpdateOwner(owner);

            Assert.That(_db.Owners.FirstOrDefault(o => o.UserId == owner.UserId).Properties, Is.Empty);
        }

        [Test]
        public void UpdateOwner_NotInDb_ThrowsError()
        {
            Owner owner = new Owner()
            {
                UserId = "UserId"
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateOwner(owner));
        }

        [Test]
        public void DeleteOwner_IsInDb_Successful()
        {
            Owner owner = new Owner()
            {
                UserId = "UserId"
            };


            _db.Owners.Add(owner);
            _db.SaveChanges();

            _queries.DeleteOwner(owner.UserId);

            Assert.That(_db.Owners.FirstOrDefault(o => o.UserId == owner.UserId), Is.Null);
        }

        [Test]
        public void DeleteOwner_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteOwner("NotAnId"));
        }
    }
}
