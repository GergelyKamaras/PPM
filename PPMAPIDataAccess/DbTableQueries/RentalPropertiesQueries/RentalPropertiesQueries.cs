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
            _db.RentalProperties.Remove(_db.RentalProperties.FirstOrDefault(p => p.Id.ToString() == id));
            _db.SaveChanges();
        }

        public void UpdateRentalProperty(RentalProperty rentalProperty)
        {
            _db.RentalProperties.Update(rentalProperty);
            _db.SaveChanges();
        }

        public RentalProperty GetRentalPropertyById(string id)
        {
            return _db.RentalProperties.FirstOrDefault(p => p.Id.ToString() == id);
        }

        public List<RentalProperty> GetRentalPropertiesByOwnerId(string id)
        {
            return _db.RentalProperties.Where(p => p.Owner.UserId == id).ToList();
        }
    }
}
