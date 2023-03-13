using Microsoft.EntityFrameworkCore;
using PPMAPI.DataAccess.DbTableQueries.TenantsQueries;
using PPMAPI.DataAccess;
using PPMModelLibrary.Models.Properties;
using PPMModelLibrary.Models.Users;
using PPMModelLibrary.Models.UtilityModels;

namespace PPMAPITests
{
    internal class TenantsQueriesTests
    {
        private PPMDbContext _db;
        private ITenantsQueries _queries;

        [SetUp]
        public void Setup()
        {
            var dbName = "testdb_" + DateTime.Now.ToFileTimeUtc();
            var options = new DbContextOptionsBuilder<PPMDbContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;
            _db = new PPMDbContext(options);

            _queries = new TenantsQueries(_db);
        }

        [Test]
        public void AddTenant_ValidInput_IsInDb()
        {
            Tenant tenant = new Tenant()
            {
                UserId = "UserId"
            };

            _queries.AddTenant(tenant);

            Assert.That(_db.Tenants.FirstOrDefault(o => o.UserId == tenant.UserId), Is.SameAs(tenant));
        }

        [Test]
        public void AddTenant_InvalidInput_ThrowsError()
        {
            Assert.Throws<InvalidOperationException>(() => _queries.AddTenant(new Tenant()));
        }

        [Test]
        public void GetTenantById_IsInDb_GetsTenant()
        {
            Tenant tenant = new Tenant()
            {
                UserId = "UserId"
            };

            _queries.AddTenant(tenant);

            Assert.That(_queries.GetTenantById(tenant.UserId), Is.SameAs(tenant));
        }

        [Test]
        public void GetTenantById_NotInDb_ReturnsNull()
        {
            Assert.That(_queries.GetTenantById("NotAnId"), Is.Null);
        }

        [Test]
        public void GetTenantByPropertyId_IsInDb_GetsTenant()
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

            Tenant tenant = new Tenant()
            {
                UserId = "UserId"
            };

            tenant.Properties.Add(property);


            _db.Tenants.Add(tenant);
            _db.SaveChanges();

            Assert.That(_queries.GetTenantByPropertyId(property.Id), Is.SameAs(tenant));
        }

        [Test]
        public void GetTenantByPropertyId_InvalidId_ReturnsEmptyList()
        {
            Assert.That(_queries.GetTenantByPropertyId(Guid.NewGuid()), Is.Null);
        }

        [Test]
        public void UpdateTenant_IsInDb_UpdatesSuccessfully()
        {
            Tenant tenant = new Tenant()
            {
                UserId = "UserId"
            };

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

            tenant.Properties.Add(property);

            _db.Tenants.Add(tenant);
            _db.SaveChanges();

            tenant.Properties = null;

            _queries.UpdateTenant(tenant);

            Assert.That(_db.Tenants.FirstOrDefault(o => o.UserId == tenant.UserId).Properties, Is.Null);
        }

        [Test]
        public void UpdateTenant_NotInDb_ThrowsError()
        {
            Tenant Tenant = new Tenant()
            {
                UserId = "UserId"
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _queries.UpdateTenant(Tenant));
        }

        [Test]
        public void DeleteTenant_IsInDb_Successful()
        {
            Tenant Tenant = new Tenant()
            {
                UserId = "UserId"
            };


            _db.Tenants.Add(Tenant);
            _db.SaveChanges();

            _queries.DeleteTenant(Tenant.UserId);

            Assert.That(_db.Tenants.FirstOrDefault(o => o.UserId == Tenant.UserId), Is.Null);
        }

        [Test]
        public void DeleteTenant_NotInDb_ThrowsError()
        {
            Assert.Throws<ArgumentNullException>(() => _queries.DeleteTenant("NotAnId"));
        }
    }
}
