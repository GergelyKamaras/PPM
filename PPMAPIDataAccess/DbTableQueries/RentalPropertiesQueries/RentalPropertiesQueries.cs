using Microsoft.EntityFrameworkCore;
using PPMAPIModelLibrary.Properties;

namespace PPMAPIDataAccess.DbTableQueries.RentalPropertiesQueries
{
    public class RentalPropertiesQueries : IRentalPropertiesQueries
    {
        private readonly PPMDbContext _db;

        public RentalPropertiesQueries(PPMDbContext db)
        {
            _db = db;
        }
        public void AddRentalProperty(RentalProperty rentalProperty)
        {
            _db.RentalProperties.Add(rentalProperty);
            _db.SaveChanges();
        }

        public void DeleteRentalProperty(string id)
        {
            var property = _db.RentalProperties
                .Include(p => p.Address)
                .Include(p => p.Costs)
                .Include(p => p.Revenues)
                .Include(p => p.ValueIncreases)
                .Include(p => p.ValueDecreases)
                .FirstOrDefault(p => p.Id.ToString() == id);

            _db.RentalProperties.Remove(property);
            _db.SaveChanges();
        }

        public void UpdateRentalProperty(RentalProperty rentalProperty)
        {
            _db.RentalProperties.Update(rentalProperty);
            _db.SaveChanges();
        }

        public RentalProperty GetRentalPropertyById(string id)
        {
            return _db.RentalProperties
                .Include(p => p.Address)
                .Include(p => p.Costs)
                .Include(p => p.Revenues)
                .Include(p => p.ValueIncreases)
                .Include(p => p.ValueDecreases)
                .FirstOrDefault(p => p.Id.ToString() == id);
        }

        public List<RentalProperty> GetRentalPropertiesByOwnerId(string id)
        {
            return _db.RentalProperties.Where(p => p.Owner.UserId == id)
                    .Include(p => p.Address)
                    .Include(p => p.Owner)
                    .Include(p => p.Tenant)
                    .Include(p => p.Costs)
                    .Include(p => p.Revenues)
                    .Include(p => p.ValueDecreases)
                    .Include(p => p.ValueIncreases)
                    .ToList();
        }
    }
}
