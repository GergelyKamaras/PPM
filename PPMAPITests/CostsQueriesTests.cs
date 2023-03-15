using Microsoft.EntityFrameworkCore;
using PPMAPIDataAccess;
using PPMAPIDataAccess.DbTableQueries.CostsQueries;
using PPMModelLibrary.Models.FinancialObjects.Transactions;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMAPITests
{
    internal class CostsQueriesTests
    {
        private PPMDbContext _db;
        private ICostsQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new CostsQueries(_db);
        }

        [Test]
        public void AddCost_ValidInput_IsInDb()
        {
            Cost cost = new Cost()
            {
                Id = 1,
                Title = "TotallyValidCost",
                Date = DateTime.Now,
                Description = "NotAFictitiousExpense",
                Value = 50
            };

            _queries.AddCost(cost);

            Assert.That(_db.Costs.FirstOrDefault(c => c.Id == cost.Id), Is.SameAs(cost));
        }

        [Test]
        public void AddCost_InvalidInput_ThrowsError()
        {
            Assert.Throws<DbUpdateException>(() => _queries.AddCost(new Cost()));
        }

        [Test]
        public void GetCostById_IsInDb_GetsCost()
        {
            Cost cost = new Cost()
            {
                Id = 1,
                Title = "TotallyValidCost",
                Date = DateTime.Now,
                Description = "NotAFictitiousExpense",
                Value = 50
            };

            _db.Costs.Add(cost);
            _db.SaveChanges();

            Assert.That(_queries.GetCostById(cost.Id), Is.SameAs(cost));
        }

        [Test]
        public void GetCostById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetCostById(1), Is.Null);
        }

        [Test]
        public void GetCostByPropertyId_UsePropertyIsInDb_GetsCost()
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

            Cost cost = new Cost()
            {
                Id = 1,
                Title = "TotallyValidCost",
                Date = DateTime.Now,
                Description = "NotAFictitiousExpense",
                Value = 50,
                Property = property
            };


            _db.Costs.Add(cost);
            _db.SaveChanges();

            Assert.That(_queries.GetCostByPropertyId(property.Id).Any(c => c.Id == cost.Id), Is.True);
        }

        [Test]
        public void GetCostByPropertyId_UseRentalPropertyIsInDb_GetsCost()
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

            Cost cost = new Cost()
            {
                Id = 1,
                Title = "TotallyValidCost",
                Date = DateTime.Now,
                Description = "NotAFictitiousExpense",
                Value = 50,
                RentalProperty = property
            };


            _db.Costs.Add(cost);
            _db.SaveChanges();

            Assert.That(_queries.GetCostByPropertyId(property.Id).Any(c => c.Id == cost.Id), Is.True);
        }

        [Test]
        public void GetCostByPropertyId_InvalidId_ReturnsEmptyList()
        {
            Assert.That(_queries.GetCostByPropertyId(Guid.NewGuid()), Is.Empty);
        }

        [Test]
        public void UpdateCost_IsInDb_UpdatesSuccessfully()
        {
            Cost cost = new Cost()
            {
                Id = 1,
                Title = "TotallyValidCost",
                Date = DateTime.Now,
                Description = "NotAFictitiousExpense",
                Value = 50
            };


            _db.Costs.Add(cost);
            _db.SaveChanges();
            
            string newTitle = "ReallyNotAScam";
            cost.Title = newTitle;

            _queries.UpdateCost(cost);

            Assert.That(_db.Costs.FirstOrDefault(c => c.Id == cost.Id).Title, Is.SameAs(newTitle));
        }

        [Test]
        public void UpdateCost_NotInDb_ThrowsError()
        {
            Cost cost = new Cost()
            {
                Id = 1,
                Title = "TotallyValidCost",
                Date = DateTime.Now,
                Description = "NotAFictitiousExpense",
                Value = 50
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateCost(cost));
        }

        [Test]
        public void DeleteCost_IsInDb_Successful()
        {
            Cost cost = new Cost()
            {
                Id = 1,
                Title = "TotallyValidCost",
                Date = DateTime.Now,
                Description = "NotAFictitiousExpense",
                Value = 50
            };


            _db.Costs.Add(cost);
            _db.SaveChanges();

            _queries.DeleteCost(cost.Id);

            Assert.That(_db.Costs.FirstOrDefault(c => c.Id == cost.Id), Is.Null);
        }

        [Test]
        public void DeleteCost_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteCost(0));
        }
    }
}
