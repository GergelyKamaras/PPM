using PPMModelLibrary.Models.Users;

namespace PPMAPIDataAccess.DbTableQueries.TenantsQueries
{
    public class TenantsQueries : ITenantsQueries
    {
        private readonly PPMDbContext _db;
        public TenantsQueries(PPMDbContext db)
        {
            _db = db;
        }

        public void AddTenant(Tenant tenant)
        {
            _db.Tenants.Add(tenant);
            _db.SaveChanges();
        }

        public void DeleteTenant(string id)
        {
            _db.Tenants.Remove(_db.Tenants.FirstOrDefault(o => o.UserId == id));
            _db.SaveChanges();
        }

        public void UpdateTenant(Tenant tenant)
        {
            _db.Tenants.Update(tenant);
            _db.SaveChanges();
        }

        public Tenant GetTenantById(string id)
        {
            return _db.Tenants.FirstOrDefault(o => o.UserId == id);
        }

        public Tenant GetTenantByPropertyId(Guid id)
        {
            return _db.Tenants.FirstOrDefault(t => t.Properties.Any(p => p.Id == id));
        }
    }
}
