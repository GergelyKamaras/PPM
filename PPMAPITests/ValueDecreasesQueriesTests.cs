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
            ValueDecrease ValueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };

            _queries.AddValueDecrease(ValueDecrease);

            Assert.That(_db.ValueDecreases.FirstOrDefault(c => c.Id == ValueDecrease.Id), Is.SameAs(ValueDecrease));
        }

        [Test]
        public void AddValueDecrease_InvalidInput_ThrowsError()
        {
            Assert.Throws<DbUpdateException>(() => _queries.AddValueDecrease(new ValueDecrease()));
        }

        [Test]
        public void GetValueDecreaseById_IsInDb_GetsValueDecrease()
        {
            ValueDecrease ValueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };

            _db.ValueDecreases.Add(ValueDecrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueDecreaseById(ValueDecrease.Id), Is.SameAs(ValueDecrease));
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
                    City = "Verycity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealProperty"
            };

            ValueDecrease ValueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50,
                Property = property
            };


            _db.ValueDecreases.Add(ValueDecrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueDecreaseByPropertyId(property.Id).Any(c => c.Id == ValueDecrease.Id), Is.True);
        }

        [Test]
        public void GetValueDecreaseByPropertyId_UseRentablePropertyIsInDb_GetsValueDecrease()
        {
            RentableProperty property = new RentableProperty()
            {
                Id = Guid.NewGuid(),
                Address = new Address()
                {
                    Id = 1,
                    Country = "VeryCountry",
                    City = "Verycity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    Number = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealProperty"
            };

            ValueDecrease ValueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50,
                RentableProperty = property
            };


            _db.ValueDecreases.Add(ValueDecrease);
            _db.SaveChanges();

            Assert.That(_queries.GetValueDecreaseByPropertyId(property.Id).Any(c => c.Id == ValueDecrease.Id), Is.True);
        }

        [Test]
        public void GetValueDecreaseByPropertyId_InvalidId_ReturnsEmptyList()
        {
            Assert.That(_queries.GetValueDecreaseByPropertyId(Guid.NewGuid()), Is.Empty);
        }

        [Test]
        public void UpdateValueDecrease_IsInDb_UpdatesSuccessfully()
        {
            ValueDecrease ValueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };


            _db.ValueDecreases.Add(ValueDecrease);
            _db.SaveChanges();

            string newTitle = "ReallyNotAScam";
            ValueDecrease.Title = newTitle;

            _queries.UpdateValueDecrease(ValueDecrease);

            Assert.That(_db.ValueDecreases.FirstOrDefault(c => c.Id == ValueDecrease.Id).Title, Is.SameAs(newTitle));
        }

        [Test]
        public void UpdateValueDecrease_NotInDb_ThrowsError()
        {
            ValueDecrease ValueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateValueDecrease(ValueDecrease));
        }

        [Test]
        public void DeleteValueDecrease_IsInDb_Successful()
        {
            ValueDecrease ValueDecrease = new ValueDecrease()
            {
                Id = 1,
                Title = "NewNeighbor",
                Date = DateTime.Now,
                Description = "Nuclear power plant built next door",
                Value = 50
            };


            _db.ValueDecreases.Add(ValueDecrease);
            _db.SaveChanges();

            _queries.DeleteValueDecrease(ValueDecrease.Id);
            _db.SaveChanges();

            Assert.That(_db.ValueDecreases.FirstOrDefault(c => c.Id == ValueDecrease.Id), Is.Null);
        }

        [Test]
        public void DeleteValueDecrease_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteValueDecrease(0));
        }
    }
}
