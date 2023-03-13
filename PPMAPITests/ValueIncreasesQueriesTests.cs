using Microsoft.EntityFrameworkCore;
using PPMAPI.DataAccess.DbTableQueries.ValueIncreasesQueries;
using PPMAPI.DataAccess;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Models.UtilityModels;
using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPITests
{
    internal class ValueIncreasesQueriesTests
    {
        private PPMDbContext _db;
        private IValueIncreasesQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new ValueIncreasesQueries(_db);
        }

        [Test]
        public void AddValueIncrease_ValidInput_IsInDb()
        {
            ValueIncrease valueIncrease = new ValueIncrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Police station built next door",
                Value = 50
            };

            _queries.AddValueIncrease(valueIncrease);

            Assert.That(_db.ValueIncreases.FirstOrDefault(c => c.Id == valueIncrease.Id), Is.SameAs(valueIncrease));
        }

        [Test]
        public void AddValueIncrease_InvalidInput_ThrowsError()
        {
            Assert.Throws<DbUpdateException>(() => _queries.AddValueIncrease(new ValueIncrease()));
        }

        [Test]
        public void GetValueIncreaseById_IsInDb_GetsValueIncrease()
        {
            ValueIncrease valueIncrease = new ValueIncrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Police station built next door",
                Value = 50
            };

            _db.ValueIncreases.Add(valueIncrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueIncreaseById(valueIncrease.Id), Is.SameAs(valueIncrease));
        }

        [Test]
        public void GetValueIncreaseById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetValueIncreaseById(1), Is.Null);
        }

        [Test]
        public void GetValueIncreaseByPropertyId_UsePropertyIsInDb_GetsValueIncrease()
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

            ValueIncrease valueIncrease = new ValueIncrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Police station built next door",
                Value = 50,
                Property = property
            };


            _db.ValueIncreases.Add(valueIncrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueIncreaseByPropertyId(property.Id).Any(c => c.Id == valueIncrease.Id), Is.True);
        }

        [Test]
        public void GetValueIncreaseByPropertyId_UseRentablePropertyIsInDb_GetsValueIncrease()
        {
            RentableProperty property = new RentableProperty()
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

            ValueIncrease valueIncrease = new ValueIncrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Police station built next door",
                Value = 50,
                RentableProperty = property
            };


            _db.ValueIncreases.Add(valueIncrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueIncreaseByPropertyId(property.Id).Any(c => c.Id == valueIncrease.Id), Is.True);
        }

        [Test]
        public void GetValueIncreaseByPropertyId_InvalidId_ReturnsEmptyList()
        {
            Assert.That(_queries.GetValueIncreaseByPropertyId(Guid.NewGuid()), Is.Empty);
        }

        [Test]
        public void UpdateValueIncrease_IsInDb_UpdatesSuccessfully()
        {
            ValueIncrease valueIncrease = new ValueIncrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Police station built next door",
                Value = 50
            };


            _db.ValueIncreases.Add(valueIncrease);
            _db.SaveChanges();

            string newTitle = "ReallyNotAScam";
            valueIncrease.Title = newTitle;

            _queries.UpdateValueIncrease(valueIncrease);

            Assert.That(_db.ValueIncreases.FirstOrDefault(c => c.Id == valueIncrease.Id).Title, Is.SameAs(newTitle));
        }

        [Test]
        public void UpdateValueIncrease_NotInDb_ThrowsError()
        {
            ValueIncrease valueIncrease = new ValueIncrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Police station built next door",
                Value = 50
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateValueIncrease(valueIncrease));
        }

        [Test]
        public void DeleteValueIncrease_IsInDb_Successful()
        {
            ValueIncrease valueIncrease = new ValueIncrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Police station built next door",
                Value = 50
            };


            _db.ValueIncreases.Add(valueIncrease);
            _db.SaveChanges();

            _queries.DeleteValueIncrease(valueIncrease.Id);
            _db.SaveChanges();

            Assert.That(_db.ValueIncreases.FirstOrDefault(c => c.Id == valueIncrease.Id), Is.Null);
        }

        [Test]
        public void DeleteValueIncrease_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteValueIncrease(0));
        }
    }
}
