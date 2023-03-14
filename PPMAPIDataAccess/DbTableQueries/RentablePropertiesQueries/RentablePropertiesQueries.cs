using PPMModelLibrary.Models.Properties;

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

        public void DeleteRentalProperty(Guid id)
        {
            _db.RentalProperties.Remove(_db.RentalProperties.FirstOrDefault(p => p.Id == id));
            _db.SaveChanges();
        }

        public void UpdateRentalProperty(RentalProperty rentalProperty)
        {
            _db.RentalProperties.Update(rentalProperty);
            _db.SaveChanges();
        }

        public RentalProperty GetRentalPropertyById(Guid id)
        {
            return _db.RentalProperties.FirstOrDefault(p => p.Id == id);
        }

        public List<RentalProperty> GetRentalPropertiesByOwnerId(string id)
        {
            return _db.RentalProperties.Where(p => p.Owner.UserId == id).ToList();
        }
    }
}
