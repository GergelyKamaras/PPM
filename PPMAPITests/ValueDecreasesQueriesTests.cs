using Microsoft.EntityFrameworkCore;
using PPMAPI.DataAccess.DbTableQueries.ValueDecreasesQueries;
using PPMAPI.DataAccess;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Models.UtilityModels;
using PPMModelLibrary.Models.ValueModifiers;

namespace PPMAPITests
{
    internal class ValueDecreasesQueriesTests
    {
        private PPMDbContext _db;
        private IValueDecreasesQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new ValueDecreasesQueries(_db);
        }

        [Test]
        public void AddValueDecrease_ValidInput_IsInDb()
        {
            ValueDecrease valueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };

            _queries.AddValueDecrease(valueDecrease);

            Assert.That(_db.ValueDecreases.FirstOrDefault(c => c.Id == valueDecrease.Id), Is.SameAs(valueDecrease));
        }

        [Test]
        public void AddValueDecrease_InvalidInput_ThrowsError()
        {
            Assert.Throws<DbUpdateException>(() => _queries.AddValueDecrease(new ValueDecrease()));
        }

        [Test]
        public void GetValueDecreaseById_IsInDb_GetsValueDecrease()
        {
            ValueDecrease valueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };

            _db.ValueDecreases.Add(valueDecrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueDecreaseById(valueDecrease.Id), Is.SameAs(valueDecrease));
        }

        [Test]
        public void GetValueDecreaseById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetValueDecreaseById(1), Is.Null);
        }

        [Test]
        public void GetValueDecreaseByPropertyId_UsePropertyIsInDb_GetsValueDecrease()
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

            ValueDecrease valueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50,
                Property = property
            };


            _db.ValueDecreases.Add(valueDecrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueDecreaseByPropertyId(property.Id).Any(c => c.Id == valueDecrease.Id), Is.True);
        }

        [Test]
        public void GetValueDecreaseByPropertyId_UseRentalPropertyIsInDb_GetsValueDecrease()
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

            ValueDecrease valueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50,
                RentalProperty = property
            };


            _db.ValueDecreases.Add(valueDecrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueDecreaseByPropertyId(property.Id).Any(c => c.Id == valueDecrease.Id), Is.True);
        }

        [Test]
        public void GetValueDecreaseByPropertyId_InvalidId_ReturnsEmptyList()
        {
            Assert.That(_queries.GetValueDecreaseByPropertyId(Guid.NewGuid()), Is.Empty);
        }

        [Test]
        public void UpdateValueDecrease_IsInDb_UpdatesSuccessfully()
        {
            ValueDecrease valueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };


            _db.ValueDecreases.Add(valueDecrease);
            _db.SaveChanges();

            string newTitle = "ReallyNotAScam";
            valueDecrease.Title = newTitle;

            _queries.UpdateValueDecrease(valueDecrease);

            Assert.That(_db.ValueDecreases.FirstOrDefault(c => c.Id == valueDecrease.Id).Title, Is.SameAs(newTitle));
        }

        [Test]
        public void UpdateValueDecrease_NotInDb_ThrowsError()
        {
            ValueDecrease valueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateValueDecrease(valueDecrease));
        }

        [Test]
        public void DeleteValueDecrease_IsInDb_Successful()
        {
            ValueDecrease valueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };


            _db.ValueDecreases.Add(valueDecrease);
            _db.SaveChanges();

            _queries.DeleteValueDecrease(valueDecrease.Id);

            Assert.That(_db.ValueDecreases.FirstOrDefault(c => c.Id == valueDecrease.Id), Is.Null);
        }

        [Test]
        public void DeleteValueDecrease_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteValueDecrease(0));
        }
    }
}
