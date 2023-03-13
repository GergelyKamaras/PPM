using Microsoft.EntityFrameworkCore;
using PPMAPI.DataAccess.DbTableQueries.RevenuesQueries;
using PPMAPI.DataAccess;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Models.Transactions;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMAPITests
{
    internal class RevenuesQueriesTests
    {
        private PPMDbContext _db;
        private IRevenuesQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new RevenuesQueries(_db);
        }

        [Test]
        public void AddRevenue_ValidInput_IsInDb()
        {
            Revenue Revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };

            _queries.AddRevenue(Revenue);

            Assert.That(_db.Revenues.FirstOrDefault(c => c.Id == Revenue.Id), Is.SameAs(Revenue));
        }

        [Test]
        public void AddRevenue_InvalidInput_ThrowsError()
        {
            Assert.Throws<DbUpdateException>(() => _queries.AddRevenue(new Revenue()));
        }

        [Test]
        public void GetRevenueById_IsInDb_GetsRevenue()
        {
            Revenue Revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };

            _db.Revenues.Add(Revenue);
            _db.SaveChanges();

            Assert.That(_queries.GetRevenueById(Revenue.Id), Is.SameAs(Revenue));
        }

        [Test]
        public void GetRevenueById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetRevenueById(1), Is.Null);
        }

        [Test]
        public void GetRevenueByPropertyId_UsePropertyIsInDb_GetsRevenue()
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

            Revenue Revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50,
                Property = property
            };


            _db.Revenues.Add(Revenue);
            _db.SaveChanges();

            Assert.That(_queries.GetRevenueByPropertyId(property.Id).Any(c => c.Id == Revenue.Id), Is.True);
        }

        [Test]
        public void GetRevenueByPropertyId_UseRentablePropertyIsInDb_GetsRevenue()
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

            Revenue Revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50,
                RentableProperty = property
            };


            _db.Revenues.Add(Revenue);
            _db.SaveChanges();

            Assert.That(_queries.GetRevenueByPropertyId(property.Id).Any(c => c.Id == Revenue.Id), Is.True);
        }

        [Test]
        public void GetRevenueByPropertyId_InvalidId_ReturnsEmptyList()
        {
            Assert.That(_queries.GetRevenueByPropertyId(Guid.NewGuid()), Is.Empty);
        }

        [Test]
        public void UpdateRevenue_IsInDb_UpdatesSuccessfully()
        {
            Revenue Revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };


            _db.Revenues.Add(Revenue);
            _db.SaveChanges();

            string newTitle = "ReallyNotAScam";
            Revenue.Title = newTitle;

            _queries.UpdateRevenue(Revenue);

            Assert.That(_db.Revenues.FirstOrDefault(c => c.Id == Revenue.Id).Title, Is.SameAs(newTitle));
        }

        [Test]
        public void UpdateRevenue_NotInDb_ThrowsError()
        {
            Revenue Revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateRevenue(Revenue));
        }

        [Test]
        public void DeleteRevenue_IsInDb_Successful()
        {
            Revenue Revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };


            _db.Revenues.Add(Revenue);
            _db.SaveChanges();

            _queries.DeleteRevenue(Revenue.Id);
            _db.SaveChanges();

            Assert.That(_db.Revenues.FirstOrDefault(c => c.Id == Revenue.Id), Is.Null);
        }

        [Test]
        public void DeleteRevenue_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteRevenue(0));
        }
    }
}
