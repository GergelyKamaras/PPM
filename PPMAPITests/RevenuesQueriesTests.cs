using Microsoft.EntityFrameworkCore;
using PPMAPIDataAccess.DbTableQueries.RevenuesQueries;
using PPMAPIDataAccess;
using PPMAPIModelLibrary.Models.UtilityModels;
using PPMAPIModelLibrary.FinancialObjects.Transactions;
using PPMAPIModelLibrary.Properties;

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
            Revenue revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };

            _queries.AddRevenue(revenue);

            Assert.That(_db.Revenues.FirstOrDefault(c => c.Id == revenue.Id), Is.SameAs(revenue));
        }

        [Test]
        public void AddRevenue_InvalidInput_ThrowsError()
        {
            Assert.Throws<DbUpdateException>(() => _queries.AddRevenue(new Revenue()));
        }

        [Test]
        public void GetRevenueById_IsInDb_GetsRevenue()
        {
            Revenue revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };

            _db.Revenues.Add(revenue);
            _db.SaveChanges();

            Assert.That(_queries.GetRevenueById(revenue.Id), Is.SameAs(revenue));
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
                    City = "VeryCity",
                    ZipCode = "9783",
                    Street = "VeryStreet",
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealProperty"
            };

            Revenue revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50,
                Property = property
            };


            _db.Revenues.Add(revenue);
            _db.SaveChanges();

            Assert.That(_queries.GetRevenueByPropertyId(property.Id).Any(c => c.Id == revenue.Id), Is.True);
        }

        [Test]
        public void GetRevenueByPropertyId_UseRentalPropertyIsInDb_GetsRevenue()
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
                    StreetNumber = 973,
                    AdditionalInfo = "Nothing Much"
                },
                Name = "RealProperty"
            };

            Revenue revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50,
                RentalProperty = property
            };


            _db.Revenues.Add(revenue);
            _db.SaveChanges();

            Assert.That(_queries.GetRevenueByPropertyId(property.Id).Any(c => c.Id == revenue.Id), Is.True);
        }

        [Test]
        public void GetRevenueByPropertyId_InvalidId_ReturnsEmptyList()
        {
            Assert.That(_queries.GetRevenueByPropertyId(Guid.NewGuid()), Is.Empty);
        }

        [Test]
        public void UpdateRevenue_IsInDb_UpdatesSuccessfully()
        {
            Revenue revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };


            _db.Revenues.Add(revenue);
            _db.SaveChanges();

            string newTitle = "ReallyNotAScam";
            revenue.Title = newTitle;

            _queries.UpdateRevenue(revenue);

            Assert.That(_db.Revenues.FirstOrDefault(c => c.Id == revenue.Id).Title, Is.SameAs(newTitle));
        }

        [Test]
        public void UpdateRevenue_NotInDb_ThrowsError()
        {
            Revenue revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateRevenue(revenue));
        }

        [Test]
        public void DeleteRevenue_IsInDb_Successful()
        {
            Revenue revenue = new Revenue()
            {
                Id = 1,
                Title = "TotallyValidRevenue",
                Date = DateTime.Now,
                Description = "NotAFictitiousIncome",
                Value = 50
            };


            _db.Revenues.Add(revenue);
            _db.SaveChanges();

            _queries.DeleteRevenue(revenue.Id);

            Assert.That(_db.Revenues.FirstOrDefault(c => c.Id == revenue.Id), Is.Null);
        }

        [Test]
        public void DeleteRevenue_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteRevenue(0));
        }
    }
}
